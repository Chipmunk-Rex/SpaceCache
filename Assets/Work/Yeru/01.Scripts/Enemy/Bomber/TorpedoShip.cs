using Code.Scripts.Entities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class TorpedoShip : EnemyBase
{
    [SerializeField] private TorpedoBullet bulletPrefab;
    [SerializeField] private Transform[] shooter;

    private EntityAttack _attackCompo;

    protected override void Awake()
    {
        base.Awake();
        _attackCompo = GetCompo<EntityAttack>();
    }

    protected override void OnInit()
    {
        base.OnInit();
    }
    
     public override void IncreaseAttack(float amount)
     {
         _statCompo.IncreaseBaseValue(attackStat, amount);
     }
            
     public override void IncreaseDefense(float amount)
     {
         _statCompo.IncreaseBaseValue(hpStat, amount);
     }
     
     public override void IncreaseSpeed(float amount)
     {
     }
    
    protected override void Attack()
    {
        if (!animator) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TorpedoWeapon")) return;
        animator.SetTrigger("attack");
    }

    public void HandleOnDead()
    {
        Die();
    }

    protected override void Die()
    {
        base.Die();
    }
    
    public void AE_FireIdx(int i)
    {
        if (!bulletPrefab || shooter == null || i < 0 || i >= shooter.Length || !shooter[i]) return;
        FireFrom(shooter[i]);
    }

    private void FireFrom(Transform shooter)
    {
        var b = Instantiate(bulletPrefab);             
        float dmg = _attackCompo.GetAttack();
        b.InitFromMuzzle(shooter, dmg);
    }
}

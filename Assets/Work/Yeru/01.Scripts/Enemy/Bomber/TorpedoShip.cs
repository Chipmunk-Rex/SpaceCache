using Code.Scripts.Entities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class TorpedoShip : EnemyBase
{
    [SerializeField] private TorpedoBulletPool bulletPool;
    [SerializeField] private Transform[] shooter;
    
    [SerializeField] private TransformEvent onShot = new TransformEvent();

    private EntityAttack _attackCompo;

    protected override void Awake()
    {
        base.Awake();
        _attackCompo = GetCompo<EntityAttack>();
        if (!bulletPool || !bulletPool.gameObject.scene.IsValid())
            {
                bulletPool = FindObjectOfType<TorpedoBulletPool>(true);
            }
    }

    protected override void OnInit()
    {
        base.OnInit();
    }
    
    public override void IncreaseAttack(float amount)
    {
        _statCompo.IncreaseBaseValue(attackStat, amount);
        Debug.Log($"{gameObject.name} : {attackStat.BaseValue}");
    }
            
    public override void IncreaseDefense(float amount)
    {
        _statCompo.IncreaseBaseValue(hpStat, amount);
        Debug.Log($"{gameObject.name} : {hpStat.BaseValue}");
    }
   
    public override void IncreaseSpeed(float amount)
    {
        _statCompo.IncreaseBaseValue(speedStat, amount);
        Debug.Log($"{gameObject.name} : {speedStat.BaseValue}");
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
        if (shooter == null || i < 0 || i >= shooter.Length || !shooter[i]) return;
        FireFrom(shooter[i]);
    }

    private void FireFrom(Transform muzzle)
    {
        if (bulletPool == null) return;
        
    
        var b = bulletPool.Get();
        if (b == null) return;
        
    
        b.InitFromMuzzle(muzzle, _attackCompo.GetAttack());
        onShot.Invoke(muzzle);
    }
}

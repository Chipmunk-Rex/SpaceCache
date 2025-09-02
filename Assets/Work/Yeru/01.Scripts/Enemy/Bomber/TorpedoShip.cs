using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class TorpedoShip : EnemyBase
{
    [SerializeField] private TorpedoBullet bulletPrefab;
    [SerializeField] private Transform[] shooter; 

    private float bonusDamage = 0f;
    private float bonusHealth = 0f;

    protected override void OnInit() { }
    
     public override void IncreaseAttack(float amount)
     {
         bonusDamage += amount;
     }
            
     public override void IncreaseDefense(float amount)
     {
         bonusHealth += amount;
         currentHealth += amount; 
     }
    
    protected override void Attack()
    {
        if (!animator) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TorpedoWeapon")) return;
        animator.SetTrigger("attack");
    }
    
    public void AE_FireAt(Transform shooter)
    {
        if (!shooter || !bulletPrefab) return;
        FireFrom(shooter);
    }
    
    public void AE_FireIdx(int i)
    {
        if (!bulletPrefab || shooter == null || i < 0 || i >= shooter.Length || !shooter[i]) return;
        FireFrom(shooter[i]);
    }

    private void FireFrom(Transform shooter)
    {
        var b = Instantiate(bulletPrefab);             
        float dmgFromSo = data.damage;
        b.InitFromMuzzle(shooter, dmgFromSo);
    }
}

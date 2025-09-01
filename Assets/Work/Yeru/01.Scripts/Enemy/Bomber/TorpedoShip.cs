using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class TorpedoShip : EnemyBase
{
    private float bonusDamage = 0f;
    private float bonusHealth = 0f;
    
    protected override void OnInit(){}
    
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
        
    }
}

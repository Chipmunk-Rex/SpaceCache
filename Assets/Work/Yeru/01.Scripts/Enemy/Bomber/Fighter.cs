using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Fighter : EnemyBase
{
    [SerializeField] private Muzzle[] muzzles;
    private float bonusDamage = 0f;
    private float bonusHealth = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnInit()
    {
        base.OnInit();
        if (muzzles == null || muzzles.Length == 0)
            muzzles = GetComponentsInChildren<Muzzle>(true);
    }
    
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
        for (int i = 0; i < muzzles.Length; i++)
            muzzles[i]?.Fire();
    }
}
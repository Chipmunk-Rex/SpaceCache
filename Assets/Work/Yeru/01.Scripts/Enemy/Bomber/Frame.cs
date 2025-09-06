using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Frame : EnemyBase
{
   [SerializeField] private Muzzle[] muzzles;

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
       _statCompo.IncreaseBaseValue(attackStat, amount);
   }
            
   public override void IncreaseDefense(float amount)
   {
       _statCompo.IncreaseBaseValue(hpStat, amount);
   }
   
   public override void IncreaseSpeed(float amount)
   {
   }
   
   public void HandleOnDead()
   {
       Die();
   }

   protected override void Die()
   {
       base.Die();
   }
   
   protected override void Attack()
   {
      for (int i = 0; i < muzzles.Length; i++)
         muzzles[i]?.Fire();
   }
}

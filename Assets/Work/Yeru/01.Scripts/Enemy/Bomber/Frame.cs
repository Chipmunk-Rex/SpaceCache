using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Frame : EnemyBase
{
   protected override void Attack()
   {
      Debug.Log("공격!! 넌 죽었다!");
   }
}

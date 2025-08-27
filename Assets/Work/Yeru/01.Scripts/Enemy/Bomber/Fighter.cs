using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Fighter : EnemyBase
{
    [SerializeField] private Muzzle[] muzzles;
   
    protected override void OnInit()
    {
        if (muzzles == null || muzzles.Length == 0)
            muzzles = GetComponentsInChildren<Muzzle>(true);
    }
   
    protected override void Attack()
    {
        for (int i = 0; i < muzzles.Length; i++)
            muzzles[i]?.Fire();
    }
}
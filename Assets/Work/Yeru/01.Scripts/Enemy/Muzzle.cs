using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;
using UnityEngine.Events;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private string PoolId = "Enemy"; 
    
    [SerializeField] private EnemyBulletPool pool;
    
    [SerializeField] private TransformEvent onShot = new TransformEvent();

    private EntityAttack _attackCompo;
    private EnemyBase owner; 

    void Awake()
    {
        owner = GetComponentInParent<EnemyBase>();
        _attackCompo = owner.GetCompo<EntityAttack>();
        if (!pool || !pool.gameObject.scene.IsValid())
        {
            var pools = FindObjectsOfType<EnemyBulletPool>(true);
            foreach (var p in pools)
            {
                if (p != null && p.PoolId == PoolId) 
                {
                    pool = p; break;
                }
            } 
            if (!pool)
            { 
                if (pools.Length == 1) pool = pools[0];
            }
        }
    }

    
    public void Fire()
    {
        if (pool == null || owner == null || owner.Data == null) return;

        var b = pool.Get();
        if (b == null) return; 
        
        float dmg = _attackCompo.GetAttack();
        b.InitFromMuzzle(transform, dmg);
        onShot.Invoke(transform);
    }
}
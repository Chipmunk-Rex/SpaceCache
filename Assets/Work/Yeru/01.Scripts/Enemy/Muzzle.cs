using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private EnemyBulletPool pool;

    private EntityAttack _attackCompo;
    private EnemyBase owner; 

    void Awake()
    {
        owner = GetComponentInParent<EnemyBase>();
        _attackCompo = owner.GetCompo<EntityAttack>();
    }

    
    public void Fire()
    {
        if (pool == null || owner == null || owner.Data == null) return;

        var b = pool.Get();
        if (b == null) return; 
        b.transform.SetParent(null, true); 
        
        float dmg = _attackCompo.GetAttack();
        b.InitFromMuzzle(transform, dmg);
    }
}
using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;
using UnityEngine.Events;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private EnemyBulletPool pool;
    
    [SerializeField] private TransformEvent onShot = new TransformEvent();

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
        
        float dmg = _attackCompo.GetAttack();
        b.InitFromMuzzle(transform, dmg);
        onShot.Invoke(transform);
    }
}
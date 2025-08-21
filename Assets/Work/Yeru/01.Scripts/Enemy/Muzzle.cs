using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private EnemyBulletPool pool;
    private EnemyBase owner; 

    void Awake()
    {
        owner = GetComponentInParent<EnemyBase>();
    }

    
    public void Fire()
    {
        if (pool == null || owner == null || owner.Data == null) return;

        var b = pool.Get();
        if (b == null) return; 
        b.transform.SetParent(null, true); 
        b.InitFromMuzzle(transform, owner.Data.damage);
    }
}
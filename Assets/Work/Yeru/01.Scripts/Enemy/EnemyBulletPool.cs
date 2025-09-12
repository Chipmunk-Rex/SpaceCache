using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField] private EnemyBullet prefab;
    [SerializeField] private int initialSize = 32;
    [SerializeField] private bool allowExpand = true;
    [SerializeField] private Transform bulletParent;   

    private readonly List<EnemyBullet> pool = new();
    private int next;

    void Awake()
    {
        if (!bulletParent) bulletParent = BulletRoot.GetOrCreate();
        for (int i = 0; i < initialSize; i++) CreateOne();
    }

    EnemyBullet CreateOne()
    {
        var b = Instantiate(prefab, bulletParent);     
        b.gameObject.SetActive(false);
        b.SetPool(this);                              
        pool.Add(b);
        return b;
    }

    public EnemyBullet Get()
    {
        int count = pool.Count;
        for (int i = 0; i < count; i++)
        {
            next = (next + 1) % count;
            var b = pool[next];
            if (!b.gameObject.activeSelf) return b;
        }
        return allowExpand ? CreateOne() : null;
    }

    public void Return(EnemyBullet b)
    {
        if (!b) return;
        b.transform.SetParent(bulletParent, false);    
        b.gameObject.SetActive(false);
    }
}

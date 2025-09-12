using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField] private EnemyBullet prefab;
    [SerializeField] private int initialSize = 32;
    [SerializeField] private bool allowExpand = true;
    
     [SerializeField] private Transform bulletParent;

    private readonly List<EnemyBullet> pool = new();

    void Awake()
    {
        if (!bulletParent)
        {
            var go = new GameObject("BulletParent");
            bulletParent = go.transform;
            bulletParent.SetParent(transform, false);
        }
        for (int i = 0; i < initialSize; i++)
            CreateOne();
    }

    EnemyBullet CreateOne()
    {
        var b = Instantiate(prefab, bulletParent);
        b.gameObject.SetActive(false);
        pool.Add(b);
        return b;
    }

    public EnemyBullet Get()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                return pool[i];
            }
        }
        
        return allowExpand ? CreateOne() : null;
    }
    public void Return(EnemyBullet b)
    {
        if (!b) return;
        b.gameObject.SetActive(false);
        b.transform.SetParent(bulletParent, false);
    }
}
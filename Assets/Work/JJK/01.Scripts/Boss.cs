using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Code.Scripts.Entities;
using Microsoft.Win32.SafeHandles;
using PSB_Lib.StatSystem;
using UnityEngine.Events;
using Random = System.Random;

public class Boss : Entity, IEntityComponent
{
    [SerializeField] private BossStatSO stat;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletPrefab2;
    [SerializeField] Transform playerPos;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] float spinSpeed = 90f;
    [SerializeField] private GameObject objectPool;

    public Transform firePoint;

    public bool IsSpin { get => isSpin; set => isSpin = value; }
    public float ReloadTime => reloadTime;

    [Header("Sprite")]
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject[] engine;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject destruction;

    List<BossPatternSO> currentPatternList = new List<BossPatternSO>();
    int currentPatternIndex = 0;
    private BossPatternSO lastPattern = null;

    [SerializeField] private StatSO hpStat;
    private EntityAttack _attackCompo;
    private EntityStat _statCompo;
    float reloadTime;
    float moveSpeed;
    float hp;
    private float damage;
    float angle;
    float currentAngle = 0f;
    Vector3 moveDir;
    GameObject[] bulletPool;
    GameObject[] bulletPool2;
    int poolSize = 150;
    bool isSpin;
    
    ObjectPooling objectPooling;

    public UnityEvent OnFire;

    public void Initialize(Entity entity)
    {
        _statCompo = entity.GetCompo<EntityStat>();
        _attackCompo = entity.GetCompo<EntityAttack>();
        playerPos = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    
    protected override void Awake()
    {
        base.Awake();
        InitBulletPool();
        InitBulletPool2();
        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    protected override void Start()
    {
        base.Start();
        ApplyStat();
        ShufflePatterns();
        NextPattern();
    }
    
    private void Update()
    {
        moveDir = playerPos.position - transform.position;
        if (!isSpin)
            Direction();
        else
        {
            float angleDelta = spinSpeed * Time.deltaTime;
            currentAngle += angleDelta;
            transform.Rotate(Vector3.back, angleDelta);
        }
    }
    
    private void FixedUpdate()
    {
        if (isSpin) return;

        if ((playerPos.position - transform.position).magnitude > 6)
        {
            transform.position += moveDir.normalized * (moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void InitBulletPool()
    {
        bulletPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.SetParent(objectPool.transform);
            bulletPool[i] = bullet;
            bullet.SetActive(false);
        }
    }

    private void InitBulletPool2()
    {
        bulletPool2 = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject missile = Instantiate(bulletPrefab2);
            missile.transform.SetParent(objectPool.transform);
            bulletPool2[i] = missile;
            missile.SetActive(false);
        }
    }

    private void ApplyStat()
    {
        reloadTime = stat.reloadTime;
        moveSpeed = stat.moveSpeed;
        hp = hpStat.Value;
        currentPatternList = stat.patterns;
    }

    public void TakeDamage()
    {
        hp -= damage;
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        main.SetActive(false);
        weapon.SetActive(false);

        for (int i = 0; i < engine.Length; i++)
        {
            engine[i].SetActive(false);
        }

        destruction.SetActive(true);

        yield return new WaitForSeconds(1.1f);

        objectPooling.ReturnBoss(stat, gameObject);
    }

    private void NextPattern()
    {
        if (currentPatternList == null || currentPatternList.Count <= 0)
            Debug.Log("덱이 비었습니다.");
        
        if (currentPatternIndex >= currentPatternList.Count)
        {
            ShufflePatterns();
        }
        
        StartCoroutine(RunPattern(currentPatternList[currentPatternIndex]));
        
        currentPatternIndex++;
    }
    
    private void ShufflePatterns()
    {
        currentPatternList = new List<BossPatternSO>(stat.patterns);
        
        for (int i = 0; i < currentPatternList.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, currentPatternList.Count);
            (currentPatternList[i], currentPatternList[rand]) = (currentPatternList[rand], currentPatternList[i]);
        }
        
        if (lastPattern != null && currentPatternList[0] == lastPattern)
        {
            (currentPatternList[0], currentPatternList[1]) = (currentPatternList[1], currentPatternList[0]);
        }

        currentPatternIndex = 0;
    }

    private IEnumerator RunPattern(BossPatternSO pattern)
    {
        bool isLaserPattern = pattern is LaserSO;

        if (isLaserPattern)
        {
            isSpin = true;
        }

        yield return StartCoroutine(pattern.Execute(this));
        yield return new WaitForSeconds(0.5f);

        if (isLaserPattern)
        {
            isSpin = false;
        }

        lastPattern = pattern;
        NextPattern();
    }

    public void ShootBullet1(float angleOffset, float speed)
    {
        GameObject bullet = GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;

            Vector3 baseDir = transform.up;
            damage = _attackCompo.GetAttack();

            Vector3 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;
            
            bullet.GetComponent<Bullet>().Init(dir, speed, damage);
            bullet.SetActive(true);
        }
    }

    public void ShootBullet2()
    {
        Vector3 right = firePoint.right;
        Vector3 up = firePoint.up;

        Vector3[] offsets = new Vector3[]
        {
            -right - up,
            Vector3.zero,
            right - up
        };

        foreach (Vector3 offset in offsets)
        {
            GameObject bullet = GetPooledBullet2();
            if (bullet != null)
            {
                float speed = 8f;
                damage = _attackCompo.GetAttack();
                bullet.transform.position = firePoint.position + offset;
                bullet.GetComponent<Bullet>().Init(transform.up, speed, damage);
                bullet.SetActive(true);
            }
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null;
    }

    public GameObject GetPooledBullet2()
    {
        for (int i = 0; i < bulletPool2.Length; i++)
        {
            if (!bulletPool2[i].activeInHierarchy)
            {
                return bulletPool2[i];
            }
        }
        
        return null;
    }

    public void ActivateLaser(bool active)
    {
        laserPrefab.SetActive(active);
        if (active)
            laserPrefab.transform.rotation = transform.rotation;
    }

    private void Direction()
    {
        Vector3 dir = playerPos.position - transform.position;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    public void FireHomingMissile()
    {
        GameObject missile = GetPooledBullet2();
        if (missile != null)
        {
            float time = 2f;
            missile.GetComponent<HomingMissile>().InitHomingTime(time);
            missile.transform.position = firePoint.position;
            missile.transform.rotation = transform.rotation;
            missile.SetActive(true);
        }
    }

}

using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Random = System.Random;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossStatSO stat;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletPrefab2;
    [SerializeField] Transform playerPos;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] float spinSpeed = 90f;

    public Transform firePoint;

    public bool IsSpin { get => isSpin; set => isSpin = value; }
    public float ReloadTime => reloadTime;

    [Header("Sprite")]
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject[] engine;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject destruction;

    List<BossPatternSO> currentPatternList;

    float reloadTime;
    float moveSpeed;
    float hp;
    float damage;
    float angle;
    float currentAngle = 0f;
    Vector3 moveDir;
    GameObject[] bulletPool;
    GameObject[] bulletPool2;
    int poolSize = 120;
    int currentPatternIndex = 0;
    bool isSpin;

    private void Start()
    {
        ApplyStat();
        NextPattern();
        InitBulletPool();
        InitBulletPool2();
    }

    private void InitBulletPool()
    {
        bulletPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
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
            bulletPool2[i] = missile;
            missile.SetActive(false);
        }
    }

    private void ApplyStat()
    {
        reloadTime = stat.reloadTime;
        moveSpeed = stat.moveSpeed;
        hp = stat.hp;
        damage = stat.damage;
        currentPatternList = stat.patterns;
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

    public void TakeDamage(float damage)
    {
        hp -= damage;
        
        if (hp <= 0)
        {
            hp = 0;
            StartCoroutine(Die());
        }
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

        Destroy(gameObject);
    }

    private void NextPattern()
    {
        StartCoroutine(RunPattern(currentPatternList[currentPatternIndex]));
        
        int newIndex = currentPatternIndex;

        while (newIndex == currentPatternIndex)
        {
            newIndex = UnityEngine.Random.Range(0, currentPatternList.Count);
        }
        
        currentPatternIndex = newIndex;
    }

    private IEnumerator RunPattern(BossPatternSO pattern)
    {
        yield return StartCoroutine(pattern.Execute(this));
        yield return new WaitForSeconds(0.5f);
        NextPattern();
    }

    public void ShootBullet1(float angleOffset, float speed)
    {
        GameObject bullet = GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;

            Vector3 baseDir = transform.up;

            Vector3 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;
            
            bullet.GetComponent<Bullet>().Init(dir, speed);
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
                bullet.transform.position = firePoint.position + offset;
                bullet.GetComponent<Bullet>().Init(transform.up, speed);
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

    private void FixedUpdate()
    {
        if ((playerPos.position - transform.position).magnitude > 6)
        {
            transform.position += moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
        }
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

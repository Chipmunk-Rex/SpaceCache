using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossStatSO stat;

    [SerializeField] Transform playerPos;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletPrefab2;
    [SerializeField] Transform firePoint;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] float spinSpeed = 90f;

    public bool IsSpin { get => isSpin; set => isSpin = value; }
    public float ReloadTime => reloadTime;

    [Header("Sprite")]
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject engine;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject destruction;

    List<BossPatternSO> currentPatternList;

    float reloadTime;
    float moveSpeed;
    float hp;
    float maxHp;
    float damage;
    float angle;
    float currentAngle = 0f;
    Vector3 moveDir;
    GameObject[] bulletPool;
    GameObject[] bulletPool2;
    int poolSize = 50;
    int currentPatternIndex = 0;
    bool isSpin;
    bool isPhase2 = false;

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
        maxHp = stat.hp;
        hp = stat.hp;
        damage = stat.damage;
        currentPatternList = stat.phase1Patterns;
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

        if (hp <= 0)
        {
            hp = 0;
            StartCoroutine(Die());
        }

        if (hp <= maxHp * 0.5f)
        {
            EnterPhase2();
        }
    }

    private void EnterPhase2()
    {
        isPhase2 = true;
        currentPatternList = stat.phase2Patterns;
        currentPatternIndex = 0;
    }

    private IEnumerator Die()
    {
        main.SetActive(false);
        engine.SetActive(false);
        weapon.SetActive(false);
        destruction.SetActive(true);

        yield return new WaitForSeconds(1.1f);

        Destroy(gameObject);
    }

    private void NextPattern()
    {
        if (currentPatternList.Count == 0) return;

        StartCoroutine(RunPattern(currentPatternList[currentPatternIndex]));
        currentPatternIndex = (currentPatternIndex + 1) % currentPatternList.Count;
    }

    private IEnumerator Pattern3()
    {
        for (int i = 0; i < 20; i++)
        {
            ShootBullet2();
            yield return new WaitForSeconds(reloadTime + 0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        NextPattern();
    }

    private IEnumerator RunPattern(BossPatternSO pattern)
    {
        yield return StartCoroutine(pattern.Execute(this));
        yield return new WaitForSeconds(0.5f);
        NextPattern();
    }

    public void ShootBullet1(float angleOffset)
    {
        GameObject bullet = GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;

            Vector3 baseDir = transform.up;

            Vector3 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;

            bullet.GetComponent<Bullet>().InitDirection(dir);
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
            GameObject bullet = GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position + offset;
                bullet.GetComponent<Bullet>().InitDirection(transform.up);
                bullet.SetActive(true);
            }
        }
    }

    private GameObject GetPooledBullet()
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

    private GameObject GetPooledBullet2()
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
            missile.transform.position = firePoint.position;
            missile.transform.rotation = transform.rotation;
            missile.SetActive(true);
        }
    }

}

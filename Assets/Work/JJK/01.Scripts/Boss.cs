using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossDataSO stat;

    [SerializeField] Transform playerPos;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] float spinSpeed = 90f;

    [Header("Sprite")]
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject engine;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject destruction;

    float reloadTime;
    float moveSpeed;
    float hp;
    float damage;

    float angle;
    float currentAngle = 0f;
    Vector3 moveDir;
    GameObject[] bulletPool;
    int poolSize = 50;
    bool canFire = true;
    bool isSpin = false;
    int patternCount = 1;

    private void Start()
    {
        bulletPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bulletPool[i] = bullet;
            bullet.SetActive(false);
        }

        ApplyStat();
        NextPattern();
    }

    private void ApplyStat()
    {
        reloadTime = stat.reloadTime;
        moveSpeed = stat.moveSpeed;
        hp = stat.hp;
        damage = stat.damage;
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
            StartCoroutine(Die());
        }
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
        switch (patternCount)
        {
            case 1:
                StartCoroutine(Pattern1());
                patternCount++;
                break;
            case 2:
                StartCoroutine(Pattern2());
                patternCount++;
                break;
            case 3:
                StartCoroutine(Pattern3());
                patternCount = 1;
                break;
        }
    }

    private IEnumerator Pattern1()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                yield return new WaitForSeconds(reloadTime);

                if (j == 0)
                {
                    ShootBullet1(0);
                }
                else
                {
                    float angle = 12 * j;
                    ShootBullet1(angle);
                    ShootBullet1(-angle);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        NextPattern();
    }

    private IEnumerator Pattern2()
    {
        isSpin = true;
        FireLaser();

        yield return new WaitForSeconds(4f);

        isSpin = false;
        laserPrefab.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        NextPattern();
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

    private void ShootBullet1(float angleOffset)
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

    private void ShootBullet2()
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

    void FireLaser()
    {
        laserPrefab.SetActive(true);
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
}

using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

public class Boss : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float reloadTime = 0.3f;

    float angle;
    public Vector3 moveDir;
    float moveSpeed = 4f;
    GameObject[] bulletPool;
    int poolSize = 20;
    bool canFire = true;
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

        NextPattern();
    }

    private void Update()
    {
        moveDir = playerPos.position - transform.position;
        Direction();

        //if (canFire)
        //    Fire();
    }

    private IEnumerator Pattern1()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                yield return new WaitForSeconds(reloadTime);

                if (j == 0)
                {
                    ShootBullet(0);
                }
                else
                {
                    float angle = 15 * j;
                    ShootBullet(angle);
                    ShootBullet(-angle);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        NextPattern();
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
                patternCount++;
                break;
            case 3:
                patternCount = 1;
                break;
        }
    }

    private void ShootBullet(float angleOffset)
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


    private void Fire()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = bulletPool[i];
            if (!bullet.activeSelf)
            {
                bullet.transform.position = firePoint.position;
                bullet.SetActive(true);

                break;
            }
        }

        StartCoroutine(Reload());
    }

    private void FixedUpdate()
    {
        if (MathF.Pow(playerPos.position.x - transform.position.x, 2) + MathF.Pow(playerPos.position.y - transform.position.y, 2) > 36)
        {
            transform.position += moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
        }
    }

    private void Direction()
    {
        angle = Mathf.Atan2(playerPos.transform.position.y - transform.position.y, playerPos.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private IEnumerator Reload()
    {
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
    }
}

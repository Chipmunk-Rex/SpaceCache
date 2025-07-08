using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

public class Boss : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject bulletPrefab;

    float angle;
    public Vector3 moveDir;
    float moveSpeed = 4f;
    GameObject[] bulletPool;
    int poolSize = 10;
    bool canFire = true;

    private void Start()
    {
        bulletPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bulletPool[i] = bullet;
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
        moveDir = playerPos.position - transform.position;
        Direction();

        if (canFire)
            Fire();
    }

    private void Fire()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = bulletPool[i];
            if (!bullet.activeSelf)
            {
                bullet.transform.position = transform.position + moveDir.normalized;
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
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }
}

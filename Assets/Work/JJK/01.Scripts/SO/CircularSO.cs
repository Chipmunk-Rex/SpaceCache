using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CircularSO", menuName = "SO/CircularSO")]
public class CircularSO : BossPatternSO
{
    public float damage = 1f;
    public int bulletCount = 12;
    public int repeatCount = 4;
    public float bulletSpeed = 5f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        float angleStep = 360f / bulletCount;

        for (int j = 0; j < repeatCount; j++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = boss.GetPooledBullet();
                if (bullet == null) continue;

                float angle = i * angleStep + j * 15;
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;

                //bullet.transform.position = origin;
                bullet.transform.position = boss.transform.position + dir.normalized;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().Init(dir, bulletSpeed, damage);
                
            }

            for (int i = 0; i < bulletCount; i++)
            {
                boss.OnFire?.Invoke();
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(boss.ReloadTime * 2f);
        }

        yield return new WaitForSeconds(waitBetweenRounds);
    }
}
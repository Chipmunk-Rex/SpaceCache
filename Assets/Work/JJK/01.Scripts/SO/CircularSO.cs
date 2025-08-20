using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CircularSO", menuName = "Scriptable Objects/CircularSO")]
public class CircularSO : BossPatternSO
{
    public int bulletCount = 12;
    public int repeatCount = 4;
    public float bulletSpeed = 5f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        float angleStep = 360f / bulletCount;
        //Vector3 origin = boss.firePoint.position;

        for (int j = 0; j < repeatCount; j++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = boss.GetPooledBullet();
                if (bullet == null) continue;

                float angle = i * angleStep + j * 15;
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;

                //bullet.transform.position = origin;
                bullet.transform.position = boss.transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().Init(dir, bulletSpeed);

                Debug.Log("shoot");

                //yield return new WaitForSeconds(boss.ReloadTime * 0.5f);
            }

            yield return new WaitForSeconds(boss.ReloadTime * 2);
        }

        yield return new WaitForSeconds(waitBetweenRounds);
    }
}
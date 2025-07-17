using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CircularSO", menuName = "Scriptable Objects/CircularSO")]
public class CircularSO : BossPatternSO
{
    public int bulletCount = 12;
    public float bulletSpeed = 5f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        GameObject bullet = boss.GetPooledBullet();

        float angleStep = 360f / bulletCount;
        Vector3 origin = boss.firePoint.position;

        if (bullet != null)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleStep;
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;

                bullet.transform.position = origin;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().Init(dir, bulletSpeed);
            }
        }
        else if (bullet == null)
        {
            Debug.Log("null");
        }

            yield return new WaitForSeconds(waitBetweenRounds);
    }
}
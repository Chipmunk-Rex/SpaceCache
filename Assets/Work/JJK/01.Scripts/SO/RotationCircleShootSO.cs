using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/RotatingCircleShot")]
public class RotationCircleShootSO : BossPatternSO
{
    public float damage = 1f;
    public int bulletCount = 12;
    public float rotationStep = 10f;
    public int roundCount = 10;
    public float bulletSpeed = 6f;
    public float delayBetweenRounds = 0.3f;

    public override IEnumerator Execute(Boss boss)
    {
        float currentRotation = 0f;

        for (int round = 0; round < roundCount; round++)
        {
            float angleStep = 360f / bulletCount;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleStep + currentRotation;
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;

                GameObject bullet = boss.GetPooledBullet();
                if (bullet != null)
                {
                    bullet.transform.position = boss.transform.position + dir.normalized;
                    bullet.GetComponent<Bullet>().Init(dir, bulletSpeed, damage);
                    bullet.SetActive(true);
                }
            }
            
            currentRotation += rotationStep;

            yield return new WaitForSeconds(delayBetweenRounds);
        }

        yield return new WaitForSeconds(1);
    }
}

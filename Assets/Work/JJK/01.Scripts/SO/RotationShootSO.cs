using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RotationShootSO", menuName = "Scriptable Objects/RotationShootSO")]
public class RotationShootSO : BossPatternSO
{
    public float rotationSpeed = 15f;
    public float duration = 5f;
    public float bulletSpeed = 5f;
    public float fireRate = 0.1f;

    public override IEnumerator Execute(Boss boss)
    {
        float elapsed = 0f;
        float currentAngle = 0f;

        while (elapsed < duration)
        {
            GameObject bullet = boss.GetPooledBullet();

            if (bullet != null)
            {
                Vector3 dir = Quaternion.Euler(0, 0, currentAngle) * Vector3.up;
                
                bullet.transform.position = boss.transform.position + dir.normalized;

                bullet.GetComponent<Bullet>().Init(dir, bulletSpeed);
                bullet.SetActive(true);
            }
            
            currentAngle += rotationSpeed;
            if (currentAngle >= 360f) currentAngle -= 360f;

            elapsed += fireRate;
            yield return new WaitForSeconds(fireRate);
        }

        yield return new WaitForSeconds(2);
    }
}

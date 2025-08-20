using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ExplosionShoot", menuName = "SO/ExplosionShoot")]
public class ExplosionShootSO : BossPatternSO
{
    public GameObject explosionBulletPrefab;
    int fireCount = 4;
    float delay = 2f;

    public override IEnumerator Execute(Boss boss)
    {
        for (int i = 0; i < fireCount; i++)
        {
            yield return new WaitForSeconds(delay);

            Vector3 dir = boss.transform.up;
            GameObject bullet = Instantiate(explosionBulletPrefab);
            bullet.transform.position = boss.firePoint.position;
            bullet.SetActive(true);
            bullet.GetComponent<ExplosionBullet>().Init(dir, 4f, boss);
        }

        yield return new WaitForSeconds(3);
    }
}

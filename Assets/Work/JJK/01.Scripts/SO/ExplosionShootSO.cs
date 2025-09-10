using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ExplosionShoot", menuName = "SO/ExplosionShoot")]
public class ExplosionShootSO : BossPatternSO
{
    public GameObject explosionBulletPrefab;
    public float moveSpeed = 4f;
    public int fireCount = 4;
    public float delay = 2f;

    public override IEnumerator Execute(Boss boss)
    {
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < fireCount; i++)
        {
            Vector3 dir = boss.transform.up;
            GameObject bullet = Instantiate(explosionBulletPrefab);
            bullet.transform.position = boss.firePoint.position;
            bullet.SetActive(true);
            bullet.GetComponent<ExplosionBullet>().Init(dir, moveSpeed, boss);
            boss.OnFire.Invoke();
            
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);
    }
}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TripleShootSO", menuName = "Scriptable Objects/TripleShootSO")]
public class TripleShootSO : BossPatternSO
{
    public int burstCount = 20;
    public float interval = 0.1f;

    public override IEnumerator Execute(Boss boss)
    {
        for (int i = 0; i < burstCount; i++)
        {
            boss.ShootBullet2();
            boss.OnFire.Invoke();
            yield return new WaitForSeconds(0.01f);
            boss.OnFire.Invoke();
            yield return new WaitForSeconds(0.01f);
            boss.OnFire.Invoke();
            yield return new WaitForSeconds(boss.ReloadTime + interval);
        }

        yield return new WaitForSeconds(0.5f);
    }
}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SpreadShoot", menuName = "Scriptable Objects/SpreadShoot")]
public class SpreadShootSO : BossPatternSO
{
    public int repeatCount = 3;
    public int bulletPerRound = 7;
    public float angleStep = 12f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            for (int j = 0; j < bulletPerRound; j++)
            {
                yield return new WaitForSeconds(boss.ReloadTime);

                if (j == 0)
                {
                    boss.ShootBullet1(0);
                }
                else
                {
                    float angle = angleStep * j;
                    boss.ShootBullet1(angle);
                    boss.ShootBullet1(-angle);
                }
            }

            yield return new WaitForSeconds(waitBetweenRounds);
        }
    }
}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ScatterShootSO", menuName = "Scriptable Objects/ScatterShootSO")]
public class ScatterShootSO : BossPatternSO
{
    public int repeatCount = 4;
    public int bulletPerRound = 5;
    public float angleStep = 10f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            for (int j = 0; j < bulletPerRound; j++)
            {
                yield return new WaitForSeconds(boss.ReloadTime);

                float angle = angleStep * j - 45;
                boss.ShootBullet1(angle);
            }
            for (int j = 0; j < bulletPerRound; j++)
            {
                yield return new WaitForSeconds(boss.ReloadTime);

                float angle = angleStep * (bulletPerRound - j) - 45;
                boss.ShootBullet1(angle);
            }
        }

        yield return new WaitForSeconds(waitBetweenRounds);
    }
}

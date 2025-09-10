using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "FollowingShootSO", menuName = "Scriptable Objects/FollowingShootSO")]
public class FollowingShootSO : BossPatternSO
{
    public int missileCount = 3;
    public float delayBetween = 0.6f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        for (int i = 0; i < missileCount; i++)
        {
            boss.FireHomingMissile();
            boss.OnFire.Invoke();
            yield return new WaitForSeconds(delayBetween);
        }

        yield return new WaitForSeconds(waitBetweenRounds);
    }
}

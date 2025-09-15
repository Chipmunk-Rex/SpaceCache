using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LaserSO", menuName = "Scriptable Objects/LaserSO")]
public class LaserSO : BossPatternSO
{
    public float laserActiveTime = 4f;
    public float waitBetweenRounds = 0.5f;

    public override IEnumerator Execute(Boss boss)
    {
        boss.OnLaserStart?.Invoke();
        boss.IsSpin = true;
        boss.ActivateLaser(true);

        yield return new WaitForSeconds(laserActiveTime);

        boss.OnLaserEnd?.Invoke();
        boss.IsSpin = false;
        boss.ActivateLaser(false);

        yield return new WaitForSeconds(waitBetweenRounds);
    }
}
        
        

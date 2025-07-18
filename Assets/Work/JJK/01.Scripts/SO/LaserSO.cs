using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LaserSO", menuName = "Scriptable Objects/LaserSO")]
public class LaserSO : BossPatternSO
{
    public float spinDuration = 4f;
    public float laserActiveTime = 4f;

    public override IEnumerator Execute(Boss boss)
    {
        boss.IsSpin = true;
        boss.ActivateLaser(true);

        yield return new WaitForSeconds(laserActiveTime);

        boss.IsSpin = false;
        boss.ActivateLaser(false);

        yield return null;
    }
}
        
        

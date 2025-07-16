using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BossPatternSO", menuName = "Scriptable Objects/BossPatternSO")]
public abstract class BossPatternSO : ScriptableObject
{
    public abstract IEnumerator Execute(Boss boss);
}

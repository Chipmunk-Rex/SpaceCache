using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossDataSo", menuName = "Scriptable Objects/BossDataSo")]
public class BossStatSO : ScriptableObject
{
    public string bossName;
    public GameObject bossPrefab;
    public float hp;
    public float damage;
    public float moveSpeed;
    public float reloadTime;

    public List<BossPatternSO> patterns;
}

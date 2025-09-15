using UnityEngine;

[CreateAssetMenu(fileName = "EnemySo", menuName = "Scriptable Objects/EnemySo")]
public class EnemySo : ScriptableObject
{
    [Header("Enemy Value")]
    public string enemyName;
    public float attackCooldown;
    public float range;
    public float engageRange;
    public float rotationSpeed;
    public GameObject prefab;
    [Header("TeleportValue")]
    public float teleportDistance;
    public float teleportRange;
    [Header("UpgradeValue")]
    public float enemyDefenseUp;
    public float enemyDamageUp;
    public float enemySpeedUp;
}
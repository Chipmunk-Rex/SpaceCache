using UnityEngine;

[CreateAssetMenu(fileName = "EnemySo", menuName = "Scriptable Objects/EnemySo")]
public class EnemySo : ScriptableObject
{
    public string enemyName;
    public float moveSpeed;
    public float attackCooldown;
    public float range;
    public float engageRange;
    public float rotationSpeed;
    public GameObject prefab;
    public float enemyDamageUp;
    public float enemyDefenseUp;
    public float enemySpeedUp;
}
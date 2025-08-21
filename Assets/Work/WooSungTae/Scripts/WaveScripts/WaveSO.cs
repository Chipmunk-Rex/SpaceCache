using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    [Header("적 기본 설정")]
    public int enemyCount; //에너미 수
    public EnemySo[] enemyType; // 소환 가능한 에너미 종류
    public float enemySpawnTime; //에너미 스폰 간격

    [Header("웨이브 설정")]
    public float waveEndTime; // 웨이브 끝나는 시간
    public bool bossSpawnTiming; // 보스 소환되는 시간
}

using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{

    [Header("웨이브 설정")]
    public float waveEndTime; // 에너미가 소환 다 된 후, 다음 라운드로 가는 시간
    public bool bossSpawnTiming; // 보스 소환되는 시간

    
       [Header("스폰")]
    public SequenceEntry[] sequence;

    [System.Serializable]
    public struct SequenceEntry
    {
        public EnemySo enemy;          // 어떤 적
        public int count;     // 몇 마리(같은 적 반복)
        public float defaultGap;    // 마리 간 공통 간격(초)
    }
    
}

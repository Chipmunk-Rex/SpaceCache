using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{

    [Header("���̺� ����")]
    public float waveEndTime; // ���ʹ̰� ��ȯ �� �� ��, ���� ����� ���� �ð�
    public float bossSpawnTiming; // ���� ��ȯ�Ǵ� �ð�
    public BossStatSO boss;
    public bool bossSpawn;
    
       [Header("����")]
    public SequenceEntry[] sequence;

    [System.Serializable]
    public struct SequenceEntry
    {
        public EnemySo enemy;          // � ��
        public int count;     // �� ����(���� �� �ݺ�)
        public float defaultGap;    // ���� �� ���� ����(��)
    }
    
}

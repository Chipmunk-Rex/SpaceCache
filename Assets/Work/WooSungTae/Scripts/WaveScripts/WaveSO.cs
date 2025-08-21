using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    [Header("�� �⺻ ����")]
    public int enemyCount; //���ʹ� ��
    public EnemySo[] enemyType; // ��ȯ ������ ���ʹ� ����
    public float enemySpawnTime; //���ʹ� ���� ����

    [Header("���̺� ����")]
    public float waveEndTime; // ���̺� ������ �ð�
    public bool bossSpawnTiming; // ���� ��ȯ�Ǵ� �ð�
}

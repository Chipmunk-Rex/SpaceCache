using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "SungtaeSO/LevelUpSO")]
public class LevelUpSO : ScriptableObject
{
    public int CardID; // ī�� ������ȣ
    public string CardDescription; // ī�� ����
    public Sprite CardImage; // ī�� �̹���
    public int level; // ī�� ������ �� ������
    public bool cardUiSpawn = false; // ó�� ī�� ui��ȯ�ϰ� ���̻� ��ȯ �ȵǰ� �ϴ� ��
    public int maxLevel; // ����� ���� ��������
}

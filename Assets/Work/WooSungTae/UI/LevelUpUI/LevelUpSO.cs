using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "SungtaeSO/LevelUpSO")]
public class LevelUpSO : ScriptableObject
{
    public int CardID; // ī�� ������ȣ
    public string Cardname; //ī�� ����
    public string CardDescription; // ī�� ����
    public Sprite CardImage; // ī�� �̹���
    public int level; // ī�� ������ �� ������
}

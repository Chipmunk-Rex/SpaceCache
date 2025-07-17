using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "SungtaeSO/LevelUpSO")]
public class LevelUpSO : ScriptableObject
{
    public int CardID; // 카드 고유번호
    public string Cardname; //카드 제목
    public string CardDescription; // 카드 설명
    public Sprite CardImage; // 카드 이미지
    public int level; // 카드 선택할 시 높아짐
}

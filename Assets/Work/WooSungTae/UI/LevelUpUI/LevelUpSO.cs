using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "SungtaeSO/LevelUpSO")]
public class LevelUpSO : ScriptableObject
{
    public int CardID; // 카드 고유번호
    public string CardDescription; // 카드 설명
    public Sprite CardImage; // 카드 이미지
    public int level; // 카드 선택할 시 높아짐
    public bool cardUiSpawn = false; // 처음 카드 ui소환하고 더이상 소환 안되게 하는 것
    public int maxLevel; // 몇까지 레벨 가능한지
}

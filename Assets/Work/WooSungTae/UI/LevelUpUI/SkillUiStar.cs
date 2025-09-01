using Code.Scripts.Items;
using UnityEngine;

public class SkillUiStar : MonoBehaviour
{
    [SerializeField] private GameObject starPosition;
    [SerializeField] private GameObject star;
    private LevelUpItemSO levelUpSO;

    public void StarInstantiate(LevelUpItemSO upSO)
    {
        levelUpSO = upSO;
        for(int i = 0; i < levelUpSO.selectCount; i++)
        Instantiate(star, starPosition.transform);
    }
}

using UnityEngine;

public class SkillUiStar : MonoBehaviour
{
    [SerializeField] private GameObject starPosition;
    [SerializeField] private GameObject star;
    private LevelUpSO levelUpSO;

    public void StarInstantiate(LevelUpSO upSO)
    {
        levelUpSO = upSO;
        for(int i = 0; i < levelUpSO.level; i++)
        Instantiate(star, starPosition.transform);
    }
}

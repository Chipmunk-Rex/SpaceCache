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

        // ���� �� ����
        foreach (Transform child in starPosition.transform)
        {
            Destroy(child.gameObject);
        }

        // ���� ����
        for (int i = 0; i < levelUpSO.selectCount; i++)
        {
            Instantiate(star, starPosition.transform);
        }
    }
}

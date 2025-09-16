using Code.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutPanelAddSkillIcon : MonoBehaviour
{
    [SerializeField] private GameObject skill;
    [SerializeField] private Transform content;
    [SerializeField] private List<LevelUpItemSO> levelUpItems;
    private Dictionary<LevelUpItemSO, GameObject> levelUpItemDic = new();

    public void ClickStarSpawn()
    {
        foreach(var levelupitem in levelUpItems)
        {
            if(levelupitem.selectCount == 1)
            {
                if(!levelUpItemDic.ContainsKey(levelupitem))
                {
                    GameObject skill = Instantiate(this.skill, content);
                    skill.GetComponent<Image>().sprite = levelupitem.SkillIcon;
                    levelUpItemDic[levelupitem] = skill;
                    SkillUiStar skillUi = skill.GetComponent<SkillUiStar>();
                    skillUi.StarInstantiate(levelupitem);
                    continue;
                }
            }
            else if(levelupitem.selectCount > 1)
            {
                Destroy(levelUpItemDic[levelupitem]);
                GameObject skill = Instantiate(this.skill, content);
                skill.GetComponent<Image>().sprite = levelupitem.SkillIcon;
                levelUpItemDic[levelupitem] = skill;
                SkillUiStar skillUi = skill.GetComponent<SkillUiStar>();
                skillUi.StarInstantiate(levelupitem);
                continue;
            }
        }
    }
}

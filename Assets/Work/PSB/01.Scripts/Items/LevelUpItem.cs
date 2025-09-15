using Code.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items
{
    public abstract class LevelUpItem : MonoBehaviour
    {
        public LevelUpItemSO levelUpItemSO;
        
        [SerializeField] protected Image skillIcon;
        [SerializeField] protected TextMeshProUGUI skillName;
        [SerializeField] protected TextMeshProUGUI skillDescription;

        private void Awake()
        {
            skillIcon.sprite = levelUpItemSO.SkillIcon;
            skillName.text = levelUpItemSO.Name;
            skillDescription.text = levelUpItemSO.Description;
        }

        public virtual void ApplyItem(Entity targetEntity)
        {
        }
        
    }
}
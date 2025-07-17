using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items
{
    public abstract class LevelUpItem : MonoBehaviour
    {
        [SerializeField] private LevelUpItemSO _levelUpItemSO;
        
        [SerializeField] private Image skillIcon;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI _skillDescription;

        private void Awake()
        {
            skillIcon.sprite = _levelUpItemSO.SkillIcon;
            skillName.text = _levelUpItemSO.Name;
            _skillDescription.text = _levelUpItemSO.Description;
        }

        public virtual void ApplyItem()
        {
        }
        
    }
}
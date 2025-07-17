using System;
using Code.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public class ItemSOCountUI : MonoBehaviour
    {
        [SerializeField] private LevelUpItemSO _itemSO;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _cntTxt;

        private void Awake()
        {
            gameObject.SetActive(false);
            _icon.sprite = _itemSO.SkillIcon;
        }
        
        public void ChangeSelectValue()
        {
            if (_itemSO.selectCount > 0)
            {
                gameObject.SetActive(true);
                _cntTxt.text = _itemSO.selectCount.ToString();
            }
        }
        
        
    }
}
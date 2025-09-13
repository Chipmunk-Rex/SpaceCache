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

        public void SetItemSO(LevelUpItemSO so)
        {
            _itemSO = so;
            _icon.sprite = so.SkillIcon;
            gameObject.SetActive(false);
        }

        public void ChangeSelectValue()
        {
            if (_itemSO != null && _itemSO.selectCount > 0)
            {
                gameObject.SetActive(true);
                _cntTxt.text = _itemSO.selectCount.ToString();
            }
        }
        
        
    }
}
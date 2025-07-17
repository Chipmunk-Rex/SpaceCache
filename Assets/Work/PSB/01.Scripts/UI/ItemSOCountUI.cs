using System;
using Code.Scripts.Items;
using TMPro;
using UnityEngine;

namespace Work.PSB._01.Scripts.UI
{
    public class ItemSOCountUI : MonoBehaviour
    {
        [SerializeField] private LevelUpItemSO _itemSO;
        [SerializeField] private TextMeshProUGUI _cntTxt;

        private void Update()
        {
            _cntTxt.text = _itemSO.selectCount.ToString();
        }
        
        
    }
}
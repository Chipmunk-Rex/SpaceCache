using PSB_Lib.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public abstract class StatUIItem : MonoBehaviour
    {
        [SerializeField] private StatSO statSO;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text valueText;

        private void Start()
        {
            Initialize(statSO);
        }

        private void Initialize(StatSO stat)
        {
            statSO = stat;
            iconImage.sprite = stat.Icon;
            nameText.text = stat.statName;
        }

        protected void UpdateValue(string value)
        {
            valueText.text = value;
        }
        
    }
}
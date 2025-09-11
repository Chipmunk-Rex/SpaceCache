using Code.Scripts.Items.Combat;
using Code.Scripts.Players;
using Code.Scripts.Players.States;
using PSB_Lib.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public class StatUIItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text valueText;

        private StatSO _stat;

        public void Initialize(StatSO stat)
        {
            _stat = stat;
            iconImage.sprite = stat.Icon;
            nameText.text = stat.statName;

            UpdateValue(_stat.Value);
            _stat.OnValueChanged += HandleValueChanged;
        }

        private void OnDestroy()
        {
            if (_stat != null)
                _stat.OnValueChanged -= HandleValueChanged;
        }

        private void HandleValueChanged(StatSO stat, float currentValue, float prevValue)
        {
            UpdateValue(currentValue);
        }

        private void UpdateValue(float value)
        {
            valueText.text = _stat.IsPercent ? $"{value * 100:F1}%" : value.ToString("F0");
        }
    }
}
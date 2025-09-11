using Code.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public class ShieldCooldownUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Image cooldownFill;
        [SerializeField] private TextMeshProUGUI cooldownText;  

        private ShieldAbility _shieldAbility;

        public void Initialize(Entity entity)
        {
            _shieldAbility = entity.GetCompo<ShieldAbility>();
        }

        private void Update()
        {
            if (_shieldAbility == null || !_shieldAbility.enabled)
            {
                Debug.Log("스킬이 없습니다.");
                if (cooldownText != null)
                    cooldownText.text = "스킬이 없습니다.";
                if (cooldownFill != null)
                    cooldownFill.fillAmount = 0f;
                return;
            }

            float current = _shieldAbility._cooldownTimer;
            float max = GetCooldownValue();

            if (cooldownFill != null)
            {
                cooldownFill.fillAmount = 1f - (current / max);
            }

            if (cooldownText != null)
            {
                if (current > 0)
                    cooldownText.text = Mathf.Ceil(current).ToString();
                else
                    cooldownText.text = "";
            }
        }


        private float GetCooldownValue()
        {
            return typeof(ShieldAbility)
                .GetField("cooldown", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_shieldAbility) as float? ?? 0f;
        }
        
        
    }
}
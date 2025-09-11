using Code.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public class MachineCooldownUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Image cooldownFill;
        [SerializeField] private TextMeshProUGUI cooldownText;  

        private MachineAbility _machineAbility;

        public void Initialize(Entity entity)
        {
            _machineAbility = entity.GetCompo<MachineAbility>();
        }

        private void Update()
        {
            if (_machineAbility == null || !_machineAbility.enabled)
            {
                Debug.Log("스킬이 없습니다.");
                if (cooldownText != null)
                    cooldownText.text = "스킬이 없습니다.";
                if (cooldownFill != null)
                    cooldownFill.fillAmount = 0f;
                return;
            }

            float current = _machineAbility._cooldownTimer;
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
            return typeof(MachineAbility)
                .GetField("cooldown", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_machineAbility) as float? ?? 0f;
        }

        
    }
}
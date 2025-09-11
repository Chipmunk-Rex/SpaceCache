using Code.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public class ShieldCooldownUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Image canUseSkill;
        [SerializeField] private Image cooldownFill;
        [SerializeField] private TextMeshProUGUI cooldownText;  

        private ShieldAbility _shieldAbility;
        private bool _forcedGray;

        public void Initialize(Entity entity)
        {
            _shieldAbility = entity.GetCompo<ShieldAbility>();
            _shieldAbility.OnClickSkill += ReturnIcon;
        }

        private void OnDestroy()
        {
            _shieldAbility.OnClickSkill -= ReturnIcon;
        }

        private void Update()
        {
            if (_shieldAbility == null || !_shieldAbility.enabled)
            {
                if (cooldownText != null)
                    cooldownText.text = "스킬이 없습니다.";
                if (cooldownFill != null)
                    cooldownFill.fillAmount = 0f;
                canUseSkill.color = Color.gray;
                return;
            }

            float current = _shieldAbility._cooldownTimer;
            float max = GetCooldownValue();

            if (cooldownFill != null)
                cooldownFill.fillAmount = 1f - (current / max);

            if (cooldownText != null)
            {
                if (current > 0) 
                {
                    cooldownText.text = Mathf.Ceil(current).ToString();
                    canUseSkill.color = Color.gray;
                    _forcedGray = false;
                }
                else
                {
                    cooldownText.text = "E";
                    canUseSkill.color = _forcedGray ? Color.gray : Color.yellow;
                }
            }
        }


        private float GetCooldownValue()
        {
            return typeof(ShieldAbility)
                .GetField("cooldown", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_shieldAbility) as float? ?? 0f;
        }

        private void ReturnIcon()
        {
            cooldownFill.fillAmount = 0f;
            _forcedGray = true;
            canUseSkill.color = Color.gray;
            Debug.Log("Use Skill");
        }
        
        
    }
}
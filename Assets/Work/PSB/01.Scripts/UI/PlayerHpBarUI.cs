using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items.UI
{
    public class PlayerHpBarUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Slider hpSlider;

        private EntityHealth _healthCompo;

        public void Initialize(Entity entity)
        {
            _healthCompo = entity.GetCompo<EntityHealth>();
        }

        private void Start()
        {
            _healthCompo.OnHealthChanged += UpdateHpBar;
            UpdateHpBar(_healthCompo.currentHealth, _healthCompo.maxHealth);
        }

        private void OnDestroy()
        {
            if (_healthCompo != null)
                _healthCompo.OnHealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar(float current, float max)
        {
            if (hpSlider == null) return;

            if (max <= 0f)
            {
                hpSlider.value = 0f;
                return;
            }

            hpSlider.maxValue = max;
            hpSlider.value = Mathf.Clamp(current, 0f, max);
        }
        
    }
}
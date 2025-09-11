using System;
using System.Collections;
using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class PlayerHpBarUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private RectTransform hpBar;

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
            if (max <= 0f)
            {
                SetXScale(hpBar, 0f);
                return;
            }

            float ratio = Mathf.Clamp01(current / max);
            SetXScale(hpBar, ratio);
        }

        private void SetXScale(RectTransform rect, float xValue)
        {
            if (rect == null) return;

            Vector3 scale = rect.localScale;
            scale.x = xValue;
            rect.localScale = scale;
        }
        
    }
}
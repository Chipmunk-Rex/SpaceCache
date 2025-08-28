using System;
using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class HealthVisual : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Sprite fullHealthSprite;
        [SerializeField] private Sprite midHealthSprite;
        [SerializeField] private Sprite lowHealthSprite;
        [SerializeField] private Sprite lastHealthSprite;
        
        private EntityHealth _health;
        private SpriteRenderer _targetRenderer;
        
        public void Initialize(Entity entity)
        {
            _health = entity.GetCompo<EntityHealth>();
            _targetRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _health.OnHealthChanged += UpdateVisual;
        }

        private void OnDisable()
        {
            _health.OnHealthChanged -= UpdateVisual;
        }

        private void UpdateVisual(float current, float max)
        {
            float ratio = current / max;
            if (ratio > 0.7f)
                _targetRenderer.sprite = fullHealthSprite;
            else if (ratio > 0.4f)
                _targetRenderer.sprite = midHealthSprite;
            else if (ratio > 0.10f)
                _targetRenderer.sprite = lowHealthSprite;
            else
            {
                _targetRenderer.sprite = lastHealthSprite;
            }
        }
        
    }
}
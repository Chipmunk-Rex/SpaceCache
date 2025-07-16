using System;
using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class HealthVisual : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private EntityHealth health;
        [SerializeField] private SpriteRenderer targetRenderer;
        [SerializeField] private Sprite fullHealthSprite;
        [SerializeField] private Sprite midHealthSprite;
        [SerializeField] private Sprite lowHealthSprite;
        [SerializeField] private Sprite lastHealthSprite;

        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        private void OnEnable()
        {
            health.OnHealthChanged += UpdateVisual;
        }

        private void OnDisable()
        {
            health.OnHealthChanged -= UpdateVisual;
        }

        private void UpdateVisual(float current, float max)
        {
            float ratio = current / max;
            if (ratio > 0.7f)
                targetRenderer.sprite = fullHealthSprite;
            else if (ratio > 0.4f)
                targetRenderer.sprite = midHealthSprite;
            else if (ratio > 0.10f)
                targetRenderer.sprite = lowHealthSprite;
            else
            {
                targetRenderer.sprite = lastHealthSprite;
            }
        }
        
    }
}
using System;
using Code.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Players
{
    public class PlayerLevelSystem : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private float maxManaPoint = 100f;
        [SerializeField] private int maxLevel = 15;
        
        [SerializeField] private float manaPerKillPercent = 5f;
        [SerializeField] private float manaPerGetPercent = 0.1f;

        private float _currentMana = 0f;
        private int _currentLevel = 0;
        
        public event Action OnLevelUp;

        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        private void Awake()
        {
            OnLevelUp += HandleLevelUp;
            healthSlider.minValue = _currentMana;
            healthSlider.maxValue = maxManaPoint;
        }

        private void OnDestroy()
        {
            OnLevelUp -= HandleLevelUp;
        }

        private void HandleLevelUp()
        {
            Debug.Log($"{_currentLevel}");
        }

        #region Temp
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentMana += 100f;
            }
        }
        
        #endregion

        private void FixedUpdate()
        {
            if (_currentLevel < maxLevel)
            {
                if (_currentMana <= maxManaPoint)
                {
                    _currentMana += 0.01f;
                }
                
                if (_currentMana >= maxManaPoint)
                {
                    _currentMana = 0f;
                    _currentLevel++;
                    OnLevelUp?.Invoke();
                }   
                healthSlider.value = _currentMana;
            }
        }

        public void KillEnemy()
        {
            if (_currentLevel >= maxLevel) return;

            _currentMana += manaPerKillPercent;
            if (_currentMana >= maxManaPoint)
            {
                _currentMana = 0f;
                _currentLevel++;
                OnLevelUp?.Invoke();
            }
            healthSlider.value = _currentMana;
        }

        public void GetManaAtItem()
        {
            if (_currentLevel >= maxLevel) return;

            _currentMana += manaPerGetPercent;
            if (_currentMana >= maxManaPoint)
            {
                _currentMana = 0f;
                _currentLevel++;
                OnLevelUp?.Invoke();
            }
            healthSlider.value = _currentMana;
        }

        
    }
}
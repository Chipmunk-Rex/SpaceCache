using System;
using Ami.BroAudio;
using Code.Scripts.Entities;
using PSB_Lib.Dependencies;
using PSB_Lib.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Players
{
    [Provide]
    public class PlayerLevelSystem : MonoBehaviour, IEntityComponent, IAfterInitialize, IDependencyProvider
    {
        [SerializeField] private Slider manaSlider;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private float maxManaPoint = 100f;
        [SerializeField] private int maxLevel = 15;

        [SerializeField] private float manaTimeGetPercent = 0.01f;
        [field : SerializeField] public float manaPerGetPercent = 1f;
        [field: SerializeField] public StatSO manaValueStat;

        [SerializeField] private SoundID levelUpSound;
        
        private float _currentMana = 0f;
        private int _currentLevel = 0;
        
        public event Action OnLevelUp;

        private Entity _entity;
        private EntityStat _statCompo;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            manaPerGetPercent = _statCompo.SubscribeStat(manaValueStat, HandleManaValueChange, 1f);
        }

        private void HandleManaValueChange(StatSO stat, float currentValue, float prevValue)
        {
            manaPerGetPercent = currentValue;
        }
        
        private void Awake()
        {
            manaSlider.minValue = _currentMana;
            manaSlider.maxValue = maxManaPoint;
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(manaValueStat, HandleManaValueChange);
        }

        #region Temp
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentMana += 100f;
            }
            levelText.text = $"{_currentLevel + 1}";
        }
        
        #endregion

        private void FixedUpdate()
        {
            if (_currentLevel >= maxLevel)
            {
                manaSlider.value = maxManaPoint;
                return;
            }

            _currentMana += manaTimeGetPercent;
            
            if (_currentMana >= maxManaPoint)
            {
                _currentMana = 0f;
                _currentLevel++;
                BroAudio.Play(levelUpSound);
                OnLevelUp?.Invoke();
            }
            
            manaSlider.value = _currentMana;
        }

        public void GetManaAtItem()
        {
            if (_currentLevel >= maxLevel) return;

            _currentMana += manaPerGetPercent;
            
            if (_currentMana >= maxManaPoint)
            {
                _currentMana = 0f;
                _currentLevel++;
                BroAudio.Play(levelUpSound);
                OnLevelUp?.Invoke();
            }
            
            manaSlider.value = _currentMana;
        }

        
    }
}
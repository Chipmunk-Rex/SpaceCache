using System;
using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        private Entity _entity;
        private EntityStat _statCompo;
        public event Action<float, float> OnHealthChanged;
        
        [field: SerializeField] public StatSO hpStat;
        [field: SerializeField] public float maxHealth;
        [field: SerializeField] public float currentHealth;
        
        public void Initialize(Entity entity)
        {
            _entity = entity; 
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            currentHealth = maxHealth = _statCompo.SubscribeStat(hpStat, HandleMaxHpChange, 10f);
        }
        
        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(hpStat, HandleMaxHpChange);
        }
        
        private void HandleMaxHpChange(StatSO stat, float currentValue, float prevValue)
        {
            float changed = currentValue - prevValue;
            maxHealth = currentValue;
            if (changed > 0)
            {
                currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxHealth);
            }
            else
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
        }
        
        public void SetFullHp()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
        
        #region Temp

        private void Update()
        {
            if (currentHealth <= 0)
                _entity.OnDeadEvent?.Invoke();
        }
        
        #endregion

        public void SetHp(float h)
        {
            currentHealth = Mathf.Clamp(currentHealth + h, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            _entity.OnHitEvent?.Invoke();
        }


    }
}
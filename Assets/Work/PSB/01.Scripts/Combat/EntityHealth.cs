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
        
        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        
        public void Initialize(Entity entity)
        {
            _entity = entity; 
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            currentHealth = maxHealth = _statCompo.SubscribeStat(hpStat, HandleMaxHPChange, 10f);
        }
        
        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(hpStat, HandleMaxHPChange);
        }
        
        private void HandleMaxHPChange(StatSO stat, float currentValue, float prevValue)
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
        
        #region Temp

        private void Update()
        {
            if (currentHealth <= 0)
                Destroy(gameObject);
        }
        
        #endregion


    }
}
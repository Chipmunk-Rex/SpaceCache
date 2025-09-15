using System;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Entities
{
    public class EntitySpeed : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        private Entity _entity;
        private EntityStat _statCompo;

        [SerializeField] private StatSO speedStat;
        [SerializeField] private float baseSpeed;
        [SerializeField] private float currentSpeed;

        public event Action<float> OnAttackChanged;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            currentSpeed = baseSpeed = 
                _statCompo.SubscribeStat(speedStat, HandleAttackChange, 10f);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(speedStat, HandleAttackChange);
        }

        private void HandleAttackChange(StatSO stat, float currentValue, float prevValue)
        {
            currentSpeed = currentValue;
            OnAttackChanged?.Invoke(currentSpeed);
        }

        public float GetSpeed()
        {
            return currentSpeed;
        }

        
    }
}
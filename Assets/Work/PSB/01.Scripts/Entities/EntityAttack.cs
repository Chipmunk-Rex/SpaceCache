using System;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Entities
{
    public class EntityAttack : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        private Entity _entity;
        private EntityStat _statCompo;

        [SerializeField] private StatSO attackStat;
        [SerializeField] private float baseAttack;
        [SerializeField] private float currentAttack;

        public event Action<float> OnAttackChanged;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            currentAttack = baseAttack = 
                _statCompo.SubscribeStat(attackStat, HandleAttackChange, 10f);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(attackStat, HandleAttackChange);
        }

        private void HandleAttackChange(StatSO stat, float currentValue, float prevValue)
        {
            currentAttack = currentValue;
            OnAttackChanged?.Invoke(currentAttack);
        }

        public float GetAttack()
        {
            return currentAttack;
        }

        public void IncreaseAttack(float amount)
        {
            _statCompo.IncreaseBaseValue(attackStat, amount);
        }
    }
}
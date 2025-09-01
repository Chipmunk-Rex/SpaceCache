using Code.Scripts.Entities;
using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "AttackSpeedDownSO", menuName = "SO/Item/AttackSpeedDownSO", order = 0)]
    public class AttackSpeedDownSO : LevelUpItemSO
    {
        private PlayerAttackCompo _attackCompo;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _attackCompo = targetEntity.GetCompo<PlayerAttackCompo>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have PlayerAttackCompo");
            
            statCompo.IncreaseBaseValue(_attackCompo.attackSpeedStat, -0.2f);
        }

    }
}
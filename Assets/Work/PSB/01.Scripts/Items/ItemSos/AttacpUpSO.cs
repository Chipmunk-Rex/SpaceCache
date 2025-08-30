using Code.Scripts.Entities;
using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "AttackUpSO", menuName = "SO/Item/AttackUp", order = 0)]
    public class AttacpUpSO : LevelUpItemSO
    {
        private PlayerAttackCompo _attackCompo;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _attackCompo = targetEntity.GetCompo<PlayerAttackCompo>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have attackCompo");

            statCompo.IncreaseBaseValue(_attackCompo.attackPowerStat, 5f);
        }
        
    }
}
using System;
using Code.Scripts.Entities;
using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class AttackLevelUpItem : LevelUpItem
    {
        private PlayerAttackCompo _attackCompo;

        public override void ApplyItem(Entity targetEntity)
        {
            _attackCompo = targetEntity.GetCompo<PlayerAttackCompo>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have attackCompo");
            Debug.Log("AttackItemSelected!!");

            statCompo.IncreaseBaseValue(_attackCompo.attackPowerStat, 5f);
            levelUpItemSO.selectCount++;
        }
        
    }
}
using Code.Scripts.Entities;
using Code.Scripts.Players;
using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class AttackSpeedDownItem : LevelUpItem
    {
        private PlayerAttackCompo _attackCompo;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _attackCompo = targetEntity.GetCompo<PlayerAttackCompo>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have PlayerAttackCompo");

            Debug.Log("ManaGetDirUpSelected!!");
            
            statCompo.IncreaseBaseValue(_attackCompo.attackSpeedStat, -0.2f);
        }
        
    }
}
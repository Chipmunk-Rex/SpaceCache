using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class GetValueUpItem : LevelUpItem
    {
        private PlayerLevelSystem _levelSystem;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _levelSystem = targetEntity.GetCompo<PlayerLevelSystem>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have PlayerLevelSystem");

            Debug.Log("ManaGetDirUpSelected!!");
            
            statCompo.IncreaseBaseValue(_levelSystem.manaValueStat, 2f);
        }
        
    }
}
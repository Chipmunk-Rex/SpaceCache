using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "GetValueUpSO", menuName = "SO/Item/GetValueUpSO", order = 0)]
    public class GetValueUpSO : LevelUpItemSO
    {
        private PlayerLevelSystem _levelSystem;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _levelSystem = targetEntity.GetCompo<PlayerLevelSystem>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have PlayerLevelSystem");
            
            statCompo.IncreaseBaseValue(_levelSystem.manaValueStat, 2f);
        }
        
    }
}
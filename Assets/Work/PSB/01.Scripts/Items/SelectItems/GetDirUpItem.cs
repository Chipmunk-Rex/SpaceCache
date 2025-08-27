using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class GetDirUpItem : LevelUpItem
    {
        private ItemMagnet _magnet;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _magnet = targetEntity.GetCompo<ItemMagnet>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have ItemMagnet");
            
            statCompo.IncreaseBaseValue(_magnet.manaRadiusStat, 1f);
        }
        
    }
}
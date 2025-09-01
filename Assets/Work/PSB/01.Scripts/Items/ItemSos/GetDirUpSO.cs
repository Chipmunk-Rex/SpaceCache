using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "GetDirUpSO", menuName = "SO/Item/GetDirUp", order = 0)]
    public class GetDirUpSO : LevelUpItemSO
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
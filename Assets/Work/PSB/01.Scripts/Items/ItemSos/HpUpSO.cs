using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "HpUpSO", menuName = "SO/Item/HpUp", order = 0)]
    public class HpUpSO : LevelUpItemSO
    {
        private EntityHealth _healthCompo;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _healthCompo = targetEntity.GetCompo<EntityHealth>();
            var statCompo = targetEntity.GetCompo<EntityStat>();
            if (statCompo == null)
                Debug.LogError("No have attackCompo");

            statCompo.IncreaseBaseValue(_healthCompo.hpStat, 50f);
            
            _healthCompo.SetFullHp();
        }
    }
}
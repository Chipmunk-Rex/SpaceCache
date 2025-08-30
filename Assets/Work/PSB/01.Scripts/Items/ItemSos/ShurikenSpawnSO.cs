using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "ShurikenSpawnSO", menuName = "SO/Item/ShurikenSpawnSO", order = 0)]
    public class ShurikenSpawnSO : LevelUpItemSO
    {
        private ShurikenAbility _shuriken;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _shuriken = targetEntity.GetCompo<ShurikenAbility>();
            
            _shuriken.UpgradeShuriken();
        }

        
    }
}
using Code.Scripts.Entities;

namespace Code.Scripts.Items
{
    public class ShurikenSpawnItem : LevelUpItemSO
    {
        private ShurikenAbility _shuriken;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _shuriken = targetEntity.GetCompo<ShurikenAbility>();
            
            _shuriken.UpgradeShuriken();
        }
        
    }
}
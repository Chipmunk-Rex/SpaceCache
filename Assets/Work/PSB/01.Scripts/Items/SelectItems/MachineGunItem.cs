using Code.Scripts.Entities;

namespace Code.Scripts.Items
{
    public class MachineGunItem : LevelUpItemSO
    {
        private MachineAbility _machineAbility;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _machineAbility = targetEntity.GetCompo<MachineAbility>();
            
            _machineAbility.gameObject.SetActive(true);
        }
        
        
        
    }
}
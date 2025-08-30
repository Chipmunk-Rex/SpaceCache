using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "MachineGunSO", menuName = "SO/Item/MachineGunSO", order = 0)]
    public class MachineGunSO : LevelUpItemSO
    {
        private MachineAbility _machineAbility;
        
        public override void ApplyItem(Entity targetEntity)
        {
            _machineAbility = targetEntity.GetCompo<MachineAbility>();
            
            _machineAbility.gameObject.SetActive(true);
        }
        
    }
}
using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items.ItemSos
{
    [CreateAssetMenu(fileName = "ShieldSO", menuName = "SO/Item/ShieldSO", order = 0)]
    public class ShieldSO : LevelUpItemSO
    {
         private ShieldAbility _playerShield;

        public override void ApplyItem(Entity targetEntity)
        {
            _playerShield = targetEntity.GetCompo<ShieldAbility>();

            _playerShield.gameObject.SetActive(true);
        }
        
    }
}
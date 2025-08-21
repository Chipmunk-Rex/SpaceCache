using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class ShieldItem : LevelUpItem
    {
        private ShieldAbility _playerShield;

        public override void ApplyItem(Entity targetEntity)
        {
            _playerShield = targetEntity.GetCompo<ShieldAbility>();

            _playerShield.gameObject.SetActive(true);
        }
    }
}
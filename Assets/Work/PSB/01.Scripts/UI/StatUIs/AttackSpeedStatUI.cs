using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class AttackSpeedStatUI : StatUIItem
    {
        [SerializeField] private PlayerAttackCompo attackCompo;
        
        private void Update()
        {
            UpdateValue($"{attackCompo.attackCooldown}");
        }

    }
}
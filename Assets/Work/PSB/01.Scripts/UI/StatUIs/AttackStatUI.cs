using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class AttackStatUI : StatUIItem
    {
        [SerializeField] private PlayerAttackCompo attackCompo;
        
        private void Update()
        {
            UpdateValue($"{attackCompo.attackPower}");
        }
        
    }
}
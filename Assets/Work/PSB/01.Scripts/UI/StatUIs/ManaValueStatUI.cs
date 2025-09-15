using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class ManaValueStatUI : StatUIItem
    {
        [SerializeField] private PlayerLevelSystem levelSystem;

        private void Update()
        {
            UpdateValue($"{levelSystem.manaPerGetPercent}");
        }
    }
}
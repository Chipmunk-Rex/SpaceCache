using System;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class ManaRadiusStatUI : StatUIItem
    {
        [SerializeField] private ItemMagnet magnet;

        private void Update()
        {
            UpdateValue($"{magnet.radius}");
        }
        
    }
}
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class MoveSpeedStatUI : StatUIItem
    {
        [SerializeField] private PlayerMovement movement;

        private void Update()
        {
            UpdateValue($"{movement.moveSpeed}");
        }
        
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace Work.PSB._01.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        
        
        public void OnMove(InputAction.CallbackContext context)
        {
            
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            
        }
        
    }
}
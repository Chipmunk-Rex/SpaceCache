using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Work.PSB._01.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnAttackPressed;

        public Vector2 MovementKey { get; private set; }
        private Controls _controls;
        
        public bool IsCanAttack { get; set; }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (IsCanAttack && context.performed)
                OnAttackPressed?.Invoke();
        }
        
    }
}
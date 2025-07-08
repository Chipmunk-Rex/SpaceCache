using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnSpeedUpPressed;
        public event Action OnSpeedDownPressed;
        public event Action OnAngleChangeLPressed;
        public event Action OnAngleChangeRPressed;
        public event Action OnAttackPressed;

        public Vector2 MovementKey { get; private set; }
        private Controls _controls;
        
        public bool IsCanAttack { get; set; }

        public bool isLHolding = false;
        public bool isRHolding = false;

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
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (IsCanAttack && context.performed)
                OnAttackPressed?.Invoke();
        }

        public void OnSpeedUp(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSpeedUpPressed?.Invoke();
        }

        public void OnSpeedDown(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSpeedDownPressed?.Invoke();
        }

        public void OnAngleChangeLeft(InputAction.CallbackContext context)
        {
            if (context.started)
                isLHolding = true;
            if (context.canceled)
                isLHolding = false;
        }

        public void OnAngleChangeRight(InputAction.CallbackContext context)
        {
            if (context.started)
                isRHolding = true;
            if (context.canceled)
                isRHolding = false;
        }

        public void CalcHoldingKey()
        {
            if (isLHolding)
                OnAngleChangeLPressed?.Invoke();
            if (isRHolding)
                OnAngleChangeRPressed?.Invoke();
        }
        
    }
}
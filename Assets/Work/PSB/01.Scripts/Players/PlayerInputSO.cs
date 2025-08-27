using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnSpeedUpPressed;
        public event Action OnSpeedDownPressed;
        public event Action OnAngleChangeLPressed;
        public event Action OnAngleChangeRPressed;
        public event Action OnAttackPressed;
        public event Action OnAttackStart;
        public event Action OnAttackStop;
        public event Action OnShieldPressed;
        public event Action OnMachinePressed;
        
        private Controls _controls;

        public bool IsCanAttack { get; set; } = true;
        public bool IsCanShield { get; set; } = false;
        public bool IsCanMachine { get; set; } = false;
        [field:SerializeField] public bool IsMachineGun { get; set; } = false;

        public bool isLHolding = false;
        public bool isRHolding = false;
        
        private string _lastKey = null;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
            IsCanAttack = true;
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!IsCanAttack) return;

            if (IsMachineGun)
            {
                if (context.started)
                {
                    OnAttackStart?.Invoke();
                }
                else if (context.canceled)
                {
                    OnAttackStop?.Invoke();
                }
            }
            else
            {
                if (context.performed)
                    OnAttackPressed?.Invoke();
            }
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
            {
                isLHolding = true;
                _lastKey = "L";
                
                isRHolding = false;
            }
            if (context.canceled)
            {
                isLHolding = false;
                if (_lastKey == "L")
                    _lastKey = null;
            }
        }

        public void OnAngleChangeRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                isRHolding = true;
                _lastKey = "R";
                
                isLHolding = false;
            }
            if (context.canceled)
            {
                isRHolding = false;
                if (_lastKey == "R")
                    _lastKey = null;
            }
        }

        public void OnShieldSkill(InputAction.CallbackContext context)
        {
            if (IsCanShield && context.performed)
                OnShieldPressed?.Invoke();
        }

        public void OnMachineSkill(InputAction.CallbackContext context)
        {
            if (IsCanMachine && context.performed)
                OnMachinePressed?.Invoke();
        }


        public void CalcHoldingKey()
        {
            if (_lastKey == "L" && isLHolding)
                OnAngleChangeLPressed?.Invoke();
            else if (_lastKey == "R" && isRHolding)
                OnAngleChangeRPressed?.Invoke();
        }
        
        public void ForceStopAttack()
        {
            OnAttackStop?.Invoke();
        }
        
    }
}
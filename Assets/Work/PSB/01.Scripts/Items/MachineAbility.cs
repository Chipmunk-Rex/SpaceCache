using System;
using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class MachineAbility : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float duration = 3f;
        [SerializeField] private float cooldown = 15f;  

        private Player _player;
        public float _cooldownTimer = 0f;
        private bool _isActive = false;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _cooldownTimer = cooldown;
            gameObject.SetActive(false);
        }

        private void Start()
        {
            _player.PlayerInput.OnMachinePressed += HandleMachineClick;
        }

        private void Update()
        {
            if (_isActive) return;

            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer < 0)
                {
                    _cooldownTimer = 0;
                    _player.PlayerInput.IsCanMachine = true;
                }
            }
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnMachinePressed -= HandleMachineClick;
        }

        private void HandleMachineClick()
        {
            if (_cooldownTimer > 0 || _isActive || !_player.PlayerInput.IsCanMachine)
            {
                Debug.Log("머신건 스킬을 사용할 수 없습니다.");
                return;
            }

            Debug.Log("머신건 발동");
            _isActive = true;
            _player.PlayerInput.IsCanMachine = false;
            _player.PlayerInput.IsMachineGun = true;

            Invoke(nameof(StopMachineGun), duration);
        }

        private void StopMachineGun()
        {
            Debug.Log("머신건 종료");
            _isActive = false;
            _player.PlayerInput.IsMachineGun = false;
            _cooldownTimer = cooldown;

            _player.PlayerInput.ForceStopAttack();
        }
        
    }
}
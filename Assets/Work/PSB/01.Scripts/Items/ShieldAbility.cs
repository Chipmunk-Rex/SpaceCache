using System;
using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class ShieldAbility : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private PlayerShield shieldPrefab;
        [SerializeField] private float cooldown = 30f;
        
        public float _cooldownTimer = 0f;
        private bool _shieldActive = false;
        
        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _cooldownTimer = cooldown;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_shieldActive) return;
            
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer < 0)
                {
                    _cooldownTimer = 0;
                    _player.PlayerInput.IsCanShield = true;
                }
            }
        }

        private void Start()
        {
            _player.PlayerInput.OnShieldPressed += HandleShieldClickTest;
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnShieldPressed -= HandleShieldClickTest;
            shieldPrefab.OnDestroyAction -= HandleShieldDestroy;
        }

        private void HandleShieldClickTest()
        {
            if (_cooldownTimer > 0 || _shieldActive)
            {
                Debug.Log("스킬을 사용할 수 없습니다. (쿨타임 or 이미 실드 있음)");
                return;
            }
            
            Debug.Log("Shield clicked");
            PlayerShield shieldInstance = Instantiate(shieldPrefab, transform);
            
            // 현재 실드 추적
            _shieldActive = true;
            _player.PlayerInput.IsCanShield = false;

            // 실드가 파괴되면 다시 쿨타임 시작
            shieldInstance.OnDestroyAction += HandleShieldDestroy;
        }

        private void HandleShieldDestroy()
        {
            _shieldActive = false;
            _player.PlayerInput.IsCanShield = true;
            _cooldownTimer = cooldown;
        }
        
        
    }
}
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
            _player.PlayerInput.OnShieldPressed += HandleShieldClick;
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnShieldPressed -= HandleShieldClick;
            shieldPrefab.OnDestroyAction -= HandleShieldDestroy;
        }

        private void HandleShieldClick()
        {
            if (_cooldownTimer > 0 || _shieldActive)
            {
                Debug.Log("스킬을 사용할 수 없습니다.");
                return;
            }
            
            Debug.Log("Shield clicked");
            PlayerShield shieldInstance = Instantiate(shieldPrefab, transform);
            
            _shieldActive = true;
            _player.PlayerInput.IsCanShield = false;
            
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
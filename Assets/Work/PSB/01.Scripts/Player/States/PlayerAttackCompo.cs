using System;
using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Player.States
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO attackSpeedStat;
        [SerializeField] private PoolItemSO bullet;
        [SerializeField] private Transform spawnPoint;
        
        [Inject] private PoolManagerMono _poolManager;
        
        private Player _player;
        private EntityStat _statCompo;
        private float _attackCooldown = 2f;
        private bool _canAttack = true;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            _attackCooldown = _statCompo.SubscribeStat(attackSpeedStat, HandleAttackSpeedChange, 2f);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(attackSpeedStat, HandleAttackSpeedChange);
        }
        
        public async void InitialCompo()
        {
            if (!_canAttack) return;

            _canAttack = false;
            
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);
            
            playerBullet.transform.position = spawnPoint.position;
            playerBullet.transform.rotation = spawnPoint.rotation;
            
            await Awaitable.WaitForSecondsAsync(_attackCooldown);
            
            _poolManager.Push(playerBullet);
            _canAttack = true;
        }

        private void HandleAttackSpeedChange(StatSO stat, float currentValue, float prevValue)
        {
            _attackCooldown = currentValue;
        }
        
        #region Temp

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HandleSpeedUp();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HandleSpeedDown();
            }
        }

        private void HandleSpeedUp()
        {
            if (_attackCooldown > 5) return;
            
            _statCompo.IncreaseBaseValue(attackSpeedStat, 1);
        }

        private void HandleSpeedDown()
        {
            if (_attackCooldown <= 0) return;
            
            _statCompo.IncreaseBaseValue(attackSpeedStat, -0.5f);
        }        
        #endregion
        
    }
}
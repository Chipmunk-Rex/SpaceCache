using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Players.States
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [Header("Serializefield")]
        [SerializeField] private StatSO attackPowerStat;
        [SerializeField] private StatSO attackSpeedStat;
        [SerializeField] private PoolItemSO bullet;
        [SerializeField] private Transform spawnPoint;
        
        [Header("Value")]
        [SerializeField] private float increaseSpeedValue = 1f;
        [SerializeField] private float decreaseSpeedValue = -0.5f;
        
        [Inject] private PoolManagerMono _poolManager;
        
        private Player _player;
        private EntityStat _statCompo;
        [SerializeField] private float _attackPower = 10f;
        private float _attackCooldown = 2f;
        private bool _canAttack = true;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            _attackPower = _statCompo.SubscribeStat(attackPowerStat, HandleAttackPowerChange, 10f);
            _attackCooldown = _statCompo.SubscribeStat(attackSpeedStat, HandleAttackSpeedChange, 2f);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(attackPowerStat, HandleAttackPowerChange);
            _statCompo.UnSubscribeStat(attackSpeedStat, HandleAttackSpeedChange);
        }
        
        public async void InitialCompo()
        {
            if (!_canAttack) return;

            _canAttack = false;
            
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);

            playerBullet.SetDamage(_attackPower);
            
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

        private void HandleAttackPowerChange(StatSO stat, float currentValue, float prevValue)
        {
            _attackPower = currentValue;
        }
        
        
    }
}
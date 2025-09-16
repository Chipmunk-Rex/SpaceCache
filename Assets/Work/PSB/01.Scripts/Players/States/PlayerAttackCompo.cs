using System;
using System.Threading.Tasks;
using Ami.BroAudio;
using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using PSB_Lib.StatSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Players.States
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [Header("SerializeField")] [field: SerializeField]
        public StatSO attackPowerStat;

        [field: SerializeField] public StatSO attackSpeedStat;
        [SerializeField] private PoolItemSO bullet;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform spawnPoint2;

        [Header("Value")] [field: SerializeField]
        public float attackPower = 10f;

        [field: SerializeField] public float attackCooldown = 2f;

        [Header("Sound")] [SerializeField] private SoundID shotSound;

        [Inject] private PoolManagerMono _poolManager;

        public UnityEvent OnAttackEvent;
        public event Action<float> OnAttackCooldownStart;
        public event Action OnAttackCooldownEnd;

        private Player _player;
        private EntityStat _statCompo;
        private bool _isAutoFiring;

        private float _nextAttackTime;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();

            if (_player != null) _player.PlayerInput.IsCanAttack = true;
        }

        public void AfterInitialize()
        {
            attackPower = _statCompo.SubscribeStat(attackPowerStat, HandleAttackPowerChange, 10f);
            attackCooldown = _statCompo.SubscribeStat(attackSpeedStat, HandleAttackSpeedChange, 2f);

            _player.PlayerInput.OnAttackPressed += FireBullet;
            _player.PlayerInput.OnAttackStart += StartAutoFire;
            _player.PlayerInput.OnAttackStop += StopAutoFire;
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(attackPowerStat, HandleAttackPowerChange);
            _statCompo.UnSubscribeStat(attackSpeedStat, HandleAttackSpeedChange);

            _player.PlayerInput.OnAttackPressed -= FireBullet;
            _player.PlayerInput.OnAttackStart -= StartAutoFire;
            _player.PlayerInput.OnAttackStop -= StopAutoFire;
        }

        private void Update()
        {
            if (_player.PlayerInput.IsAttackPressed)
            {
                Debug.Log("ww");
                FireBullet();
            }
        }

        public void FireBullet()
        {
            if (Time.time < _nextAttackTime) return;

            SpawnBullet(spawnPoint);
            SpawnBullet(spawnPoint2);

            float delay = _player.PlayerInput.IsMachineGun ? 0.05f : attackCooldown;
            _nextAttackTime = Time.time + delay;

            BroAudio.Play(shotSound);
            OnAttackEvent?.Invoke();
            OnAttackCooldownStart?.Invoke(delay);
            _ = ResetAttackCooldown(delay);
        }

        private async void StartAutoFire()
        {
            if (_isAutoFiring) return;
            _isAutoFiring = true;

            while (_isAutoFiring)
            {
                FireBullet();
                float delay = _player.PlayerInput.IsMachineGun ? 0.05f : attackCooldown;
                await Awaitable.WaitForSecondsAsync(delay);
            }
        }

        private void StopAutoFire()
        {
            _isAutoFiring = false;
        }

        private void SpawnBullet(Transform point)
        {
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);
            playerBullet.SetDamage(attackPower);
            playerBullet.transform.SetPositionAndRotation(point.position, point.rotation);
        }

        private async Task ResetAttackCooldown(float delay)
        {
            await Awaitable.WaitForSecondsAsync(delay);
            OnAttackCooldownEnd?.Invoke();
        }

        private void HandleAttackSpeedChange(StatSO stat, float currentValue, float prevValue)
        {
            attackCooldown = currentValue;
        }

        private void HandleAttackPowerChange(StatSO stat, float currentValue, float prevValue)
        {
            attackPower = currentValue;
        }
    }
}
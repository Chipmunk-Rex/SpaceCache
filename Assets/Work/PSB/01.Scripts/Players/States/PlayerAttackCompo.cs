using System;
using System.Threading.Tasks;
using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using PSB_Lib.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Players.States
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [Header("SerializeField")]
        [field: SerializeField] public StatSO attackPowerStat;
        [field: SerializeField] public StatSO attackSpeedStat;
        [SerializeField] private PoolItemSO bullet;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform spawnPoint2;
        
        [Header("Value")]
        [SerializeField] private float attackPower = 10f;
        [field: SerializeField] public float attackCooldown = 2f;

        [Header("UI")] 
        [SerializeField] private TextMeshProUGUI attackPowerTxt;
        
        [Inject] private PoolManagerMono _poolManager;

        public UnityEvent OnAttackEvent;
        public event Action<float> OnAttackCooldownStart;
        public event Action OnAttackCooldownEnd;
        
        private Player _player;
        private EntityStat _statCompo;
        private bool _canAttack1 = true;
        private bool _canAttack2 = true;
        private bool _isAutoFiring;
        
        private float _lastAttackTime;
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
        
        private void Update()
        {
            attackPowerTxt.text = "총알 한 발당 데미지 : " + attackPower;
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(attackPowerStat, HandleAttackPowerChange);
            _statCompo.UnSubscribeStat(attackSpeedStat, HandleAttackSpeedChange);
            
            _player.PlayerInput.OnAttackPressed -= FireBullet;
            _player.PlayerInput.OnAttackStart -= StartAutoFire;
            _player.PlayerInput.OnAttackStop -= StopAutoFire;
        }

        public void FireBullet()
        {
            if (Time.time < _nextAttackTime) return;

            InitialCompo();
            InitialCompo2();

            _lastAttackTime = Time.time;
            float delay = _player.PlayerInput.IsMachineGun ? 0.05f : attackCooldown;
            _nextAttackTime = Time.time + delay;

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
            Debug.Log("StopAutoFire");
            _isAutoFiring = false;
        }
        
        private void InitialCompo()
        {
            if (!_canAttack1) return;
            _canAttack1 = false;

            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);
            playerBullet.SetDamage(attackPower);
            playerBullet.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }

        private void InitialCompo2()
        {
            if (!_canAttack2) return;
            _canAttack2 = false;
            
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);
            playerBullet.SetDamage(attackPower);
            playerBullet.transform.SetPositionAndRotation(spawnPoint2.position, spawnPoint2.rotation);
        }

        private async Task ResetAttackCooldown(float delay)
        {
            await Awaitable.WaitForSecondsAsync(delay);
            _canAttack1 = true;
            _canAttack2 = true;

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

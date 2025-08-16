using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using PSB_Lib.StatSystem;
using TMPro;
using UnityEngine;

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
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private float increaseSpeedValue = 1f;
        [SerializeField] private float decreaseSpeedValue = -0.5f;

        [Header("UI")] 
        [SerializeField] private TextMeshProUGUI attackPowerTxt;
        
        [Inject] private PoolManagerMono _poolManager;
        
        private Player _player;
        private EntityStat _statCompo;
        private bool _canAttack1 = true;
        private bool _canAttack2 = true;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            attackPower = _statCompo.SubscribeStat(attackPowerStat, HandleAttackPowerChange, 10f);
            attackCooldown = _statCompo.SubscribeStat(attackSpeedStat, HandleAttackSpeedChange, 2f);
        }
        
        private void Update()
        {
            attackPowerTxt.text = "One bullet Damage : " + attackPower;
        }


        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(attackPowerStat, HandleAttackPowerChange);
            _statCompo.UnSubscribeStat(attackSpeedStat, HandleAttackSpeedChange);
        }

        public void InitialBullet()
        {
            InitialCompo();
            InitialCompo2();
        }
        
        public async void InitialCompo()
        {
            if (!_canAttack1) return;
            
            _canAttack1 = false;
            
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);

            playerBullet.SetDamage(attackPower);
            
            playerBullet.transform.position = spawnPoint.position;
            playerBullet.transform.rotation = spawnPoint.rotation;
            
            await Awaitable.WaitForSecondsAsync(attackCooldown);
            
            _poolManager.Push(playerBullet);
            _canAttack1 = true;
        }

        public async void InitialCompo2()
        {
            if (!_canAttack2) return;
            
            _canAttack2 = false;
            
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);

            playerBullet.SetDamage(attackPower);
            
            playerBullet.transform.position = spawnPoint2.position;
            playerBullet.transform.rotation = spawnPoint2.rotation;
            
            await Awaitable.WaitForSecondsAsync(attackCooldown);
            
            _poolManager.Push(playerBullet);
            _canAttack2 = true;
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
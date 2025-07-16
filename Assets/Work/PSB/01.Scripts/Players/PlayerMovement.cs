using System;
using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;


namespace Code.Scripts.Players
{
    public class PlayerMovement : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO moveSpeedStat;
        [SerializeField] private Rigidbody2D rigid2D;
        [SerializeField] private PlayerLookCam lookCam;
        
        [SerializeField] private float increaseSpeedValue = 5f;
        [SerializeField] private float decreaseSpeedValue = -5f;
        
        public float moveSpeed = 10f;
        public bool isRunning = true;
        
        private Player _player;
        private EntityStat _statCompo;
        private Vector2 _velocity;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            moveSpeed = _statCompo.SubscribeStat(moveSpeedStat, HandleMoveSpeedChange, 8f);
            _player.PlayerInput.OnSpeedUpPressed += HandleSpeedUp;
            _player.PlayerInput.OnSpeedDownPressed += HandleSpeedDown;
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(moveSpeedStat, HandleMoveSpeedChange);
            _player.PlayerInput.OnSpeedUpPressed -= HandleSpeedUp;
            _player.PlayerInput.OnSpeedDownPressed -= HandleSpeedDown;
        }
        
        private void FixedUpdate()
        {
            CalculateMovement();
            Move();
        }

        private void HandleMoveSpeedChange(StatSO stat, float currentValue, float prevValue)
        {
            moveSpeed = currentValue;
        }

        private void HandleSpeedUp()
        {
            if (moveSpeed > 30) return;
            
            _statCompo.IncreaseBaseValue(moveSpeedStat, increaseSpeedValue);
        }

        private void HandleSpeedDown()
        {
            if (moveSpeed <= 0) return;
            
            _statCompo.IncreaseBaseValue(moveSpeedStat, decreaseSpeedValue);
        }

        private void CalculateMovement()
        {
            _velocity = lookCam.transform.up * moveSpeed;

            if (_velocity.sqrMagnitude > 0.001f)
            {
                transform.up = _velocity;
            }
        }

        private void Move()
        {
            if (!isRunning) return;
            
            Vector2 move = _velocity * Time.fixedDeltaTime;
            rigid2D.MovePosition(rigid2D.position + move);
        }

        public void StopImmediately()
        {
            isRunning = false;
            _velocity = Vector2.zero;
        }
        
        
    }
}
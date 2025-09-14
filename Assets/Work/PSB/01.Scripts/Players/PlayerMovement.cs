using System;
using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;


namespace Code.Scripts.Players
{
    public class PlayerMovement : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [field: SerializeField] public StatSO moveSpeedStat;
        [SerializeField] private Rigidbody2D rigid2D;
        [SerializeField] private PlayerLookCam lookCam;

        public float moveSpeed = 10f;
        public float acceleration = 20f; // 가속도
        public float deceleration = 25f; // 감속도
        public bool isRunning = true;

        private Player _player;
        private EntityStat _statCompo;
        // private Vector2 _velocity;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            moveSpeed = _statCompo.SubscribeStat(moveSpeedStat, HandleMoveSpeedChange, 8f);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(moveSpeedStat, HandleMoveSpeedChange);
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

        private void CalculateMovement()
        {
            // _velocity = lookCam.transform.up * moveSpeed;
            //
            // if (_velocity.sqrMagnitude > 0.001f)
            // {
            //     transform.up = _velocity;
            // }
        }

        private void Move()
        {
            if (!isRunning) return;
            Vector2 currentVelocity = rigid2D.linearVelocity;
            Vector2 targetDirection = lookCam.transform.up;
            float currentSpeed = Vector2.Dot(currentVelocity, targetDirection);

            if (_player.PlayerInput.speedUp)
            {
                currentSpeed += acceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed);
            }
            else if (_player.PlayerInput.speedDown)
            {
                currentSpeed -= deceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed);
            }
            else
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= deceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0);
                }
            }

            rigid2D.linearVelocity = targetDirection * currentSpeed;
        }

        public void StopImmediately()
        {
            isRunning = false;
            rigid2D.linearVelocity = Vector2.zero;
        }
    }
}
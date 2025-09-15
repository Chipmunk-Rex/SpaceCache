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
            Vector2 velocity = rigid2D.linearVelocity;
            // 가속
            if (_player.PlayerInput.IsMoving)
            {
                velocity += (Vector2)lookCam.transform.up * acceleration * Time.fixedDeltaTime;
                if (velocity.magnitude > moveSpeed)
                    velocity = velocity.normalized * moveSpeed;
            }
            // 감속
            // else if (_player.PlayerInput.speedDown)
            else
            {
                if (velocity.magnitude > 0)
                {
                    velocity -= velocity.normalized * deceleration * Time.fixedDeltaTime;
                    if (Vector2.Dot(velocity, velocity) < 0.01f) // 너무 느려지면 정지
                        velocity = Vector2.zero;
                }
            }

            // 입력 없을 때 자연 감속(관성 유지)
            rigid2D.linearVelocity = velocity;
        }

        public void StopImmediately()
        {
            isRunning = false;
            rigid2D.linearVelocity = Vector2.zero;
        }
    }
}
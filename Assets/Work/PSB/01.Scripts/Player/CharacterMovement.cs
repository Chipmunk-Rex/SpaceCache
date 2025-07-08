using System;
using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class CharacterMovement : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO moveSpeedStat;
        [SerializeField] private Rigidbody2D rigid2D;
        [SerializeField] private CharacterLookCam lookCam;

        private EntityStat _statCompo;
        private float _moveSpeed = 8f;
        private Vector2 _velocity;

        private Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            _moveSpeed = _statCompo.SubscribeStat(moveSpeedStat, HandleMoveSpeedChange, 8f);
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
            _moveSpeed = currentValue;
        }

        private void CalculateMovement()
        {
            _velocity = lookCam.transform.up * _moveSpeed;

            if (_velocity.sqrMagnitude > 0.001f)
            {
                transform.up = _velocity;
            }
        }

        private void Move()
        {
            Vector2 move = _velocity * Time.fixedDeltaTime;
            rigid2D.MovePosition(rigid2D.position + move);
        }

        public void StopImmediately()
        {
            _velocity = Vector2.zero;
        }
        
        
    }
}
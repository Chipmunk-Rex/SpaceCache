﻿using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Player.States
{
    public class PlayerMoveState : PlayerCanAttackState
    {
        
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _player.visual.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            Vector2 movementKey = _player.PlayerInput.MovementKey;
            
            _movement.SetMovementDirection(movementKey);
            
            if (movementKey.magnitude < _inputThreshold)
                _player.ChangeState("IDLE");
        }

        public override void Exit()
        {
            _player.visual.SetActive(false);
            base.Exit();
        }
    }
}
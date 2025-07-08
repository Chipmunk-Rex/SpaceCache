using Code.Scripts.Entities;
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
            if (_player.PlayerInput.isLHolding && _player.PlayerInput.isRHolding)
            {
                _movement.StopImmediately();
                _player.ChangeState("IDLE");
            }
        }

        public override void Exit()
        {
            _player.visual.SetActive(false);
            base.Exit();
        }
    }
}
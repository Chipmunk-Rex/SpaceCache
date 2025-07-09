using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Player.States
{
    public class PlayerIdleState : PlayerCanAttackState
    {
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _movement.StopImmediately();
        }
        
        public override void Update()
        {
            base.Update();
            if (!_player.PlayerInput.isLHolding && !_player.PlayerInput.isRHolding)
            {
                _movement.isRunning = true;
                _player.ChangeState("MOVE");
            }
        }

        
    }
}
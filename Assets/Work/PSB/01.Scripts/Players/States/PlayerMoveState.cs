using Code.Scripts.Entities;

namespace Code.Scripts.Players.States
{
    public class PlayerMoveState : PlayerCanAttackState
    {
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Update()
        {
            base.Update();
            if (_player.PlayerInput.isLHolding && _player.PlayerInput.isRHolding)
            {
                _player.ChangeState("IDLE");
            }
        }

        
    }
}
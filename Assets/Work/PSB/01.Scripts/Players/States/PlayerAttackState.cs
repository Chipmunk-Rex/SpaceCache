using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Players.States
{
    public class PlayerAttackState : PlayerCanAttackState
    {
        private PlayerAttackCompo _attackCompo;
        
        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _attackCompo.FireBullet();
        }

        public override void Update()
        {
            base.Update();
    
            if (_isTriggerCall)
            {
                if (_player.PlayerInput.isLHolding || _player.PlayerInput.isRHolding)
                {
                    _player.ChangeState("MOVE");
                }
                else
                {
                    _player.ChangeState("IDLE");
                }
            }

        }
        
    }
}
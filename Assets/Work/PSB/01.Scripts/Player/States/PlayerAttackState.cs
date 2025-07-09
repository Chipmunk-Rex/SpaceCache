using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Player.States
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
            _attackCompo.InitialCompo();
        }

        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
            {
                _player.ChangeState("MOVE");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        
    }
}
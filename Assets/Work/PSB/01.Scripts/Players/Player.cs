using Code.Scripts.Entities;
using Code.Scripts.FSM;
using PSB_Lib.Dependencies;
using UnityEngine;

namespace Code.Scripts.Players
{
    [Provide]
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;

        private EntityStateMachine _stateMachine;
        private EntityAnimator _animator;

        int _rotateHash = Animator.StringToHash("Rotate");

        protected override void Awake()
        {
            base.Awake();
            _animator = this.GetCompo<EntityAnimator>();
            // _stateMachine = new EntityStateMachine(this, states);
        }


        protected override void Start()
        {
            // _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            // _stateMachine.UpdateStateMachine();
            PlayerInput.CalcHoldingKey();
        }


        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
    }
}
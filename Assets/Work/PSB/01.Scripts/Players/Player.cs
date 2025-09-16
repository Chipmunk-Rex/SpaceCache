using System;
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

        [SerializeField] private ParticleSystem collisionEffect;

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            Vector2 position = other.contacts[0].point;
            float rotationOffset = 180;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward,
                Quaternion.AngleAxis(rotationOffset, Vector3.forward) * other.contacts[0].normal);
            Instantiate(collisionEffect, position, rotation).gameObject.SetActive(true);
        }

        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
    }
}
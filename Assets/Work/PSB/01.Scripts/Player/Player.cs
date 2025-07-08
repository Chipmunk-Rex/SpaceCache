using Code.Scripts.Entities;
using Code.Scripts.FSM;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        [SerializeField] private StateDataSO[] states;

        private EntityStateMachine _stateMachine;
        
        #region Temp

        [field : SerializeField] public GameObject visual;
        
        #endregion 
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            visual.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            
        }

        protected override void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
        
        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);


        
    }
}
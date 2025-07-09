using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerLookCam : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float rotationSpeed = 90f;

        private float _currentAngle = 0f;

        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }
        
        private void Start()
        {
            _currentAngle = transform.eulerAngles.z;
        }

        private void OnEnable()
        {
            _player.PlayerInput.OnAngleChangeLPressed += HandleRotateLeft;
            _player.PlayerInput.OnAngleChangeRPressed += HandleRotateRight;
        }

        private void OnDisable()
        {
            _player.PlayerInput.OnAngleChangeLPressed -= HandleRotateLeft;
            _player.PlayerInput.OnAngleChangeRPressed -= HandleRotateRight;
        }

        private void FixedUpdate()
        {
            ApplyAngleRotation();
        }
        
        private void ApplyAngleRotation()
        {
            Vector2 dir = AngleToDirection(_currentAngle);
            transform.up = dir;
        }

        private Vector2 AngleToDirection(float angle)
        {
            float rad = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        }

        private void HandleRotateLeft()
        {
            _currentAngle += rotationSpeed * Time.fixedDeltaTime;
        }

        private void HandleRotateRight()
        {
            _currentAngle -= rotationSpeed * Time.fixedDeltaTime;
        }


        
    }
}
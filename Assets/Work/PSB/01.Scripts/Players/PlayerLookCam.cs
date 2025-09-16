using Code.Scripts.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Players
{
    public class PlayerLookCam : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float rotationSpeed = 90f;

        private float _currentAngle = 0f;

        private Player _player;
        private EntityAnimator _animator;
        private int _rotateHash;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            if (_player != null)
            {
                _animator = _player.GetCompo<EntityAnimator>();
                _rotateHash = Animator.StringToHash("Rotate");
            }
        }

        private void Start()
        {
            _currentAngle = transform.eulerAngles.z;
        }

        private void OnEnable()
        {
            // _player.PlayerInput.OnAngleChangeLPressed += HandleRotateLeft;
            // _player.PlayerInput.OnAngleChangeRPressed += HandleRotateRight;
        }

        private void OnDisable()
        {
            // _player.PlayerInput.OnAngleChangeLPressed -= HandleRotateLeft;
            // _player.PlayerInput.OnAngleChangeRPressed -= HandleRotateRight;
        }

        private void FixedUpdate()
        {
            if (Keyboard.current.altKey.isPressed)
            {
                UpdateAnimatorRotateParam(0, 0);
                return;
            }

            float prevAngle = _currentAngle;
            RotateTowardsMouse();
            ApplyAngleRotation();
            UpdateAnimatorRotateParam(prevAngle, _currentAngle);
        }

        private void UpdateAnimatorRotateParam(float prevAngle, float currentAngle)
        {
            if (_animator == null) return;
            float delta = Mathf.DeltaAngle(prevAngle, currentAngle);
            int rotateDir = 0;
            if (Mathf.Abs(delta) > 0.15f)
                rotateDir = delta > 0 ? -1 : 1;
            _animator.SetParam(_rotateHash, rotateDir);
        }

        private void RotateTowardsMouse()
        {
            if (_player == null || Camera.main == null) return;
            Vector2 mouseScreenPos = _player.PlayerInput.mousePosition;
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(mouseScreenPos.x, mouseScreenPos.y));
            Vector2 dir = (mouseWorldPos - (Vector2)transform.position);
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _currentAngle =
                Mathf.MoveTowardsAngle(_currentAngle, targetAngle, rotationSpeed * 360 * Time.fixedDeltaTime);
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
    }
}
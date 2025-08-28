using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerWeapon : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float angleOffset = 90f;
        private Camera _camera;
        
        public void Initialize(Entity entity)
        {
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_camera != null)
            {
                Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - transform.position);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle - angleOffset);
            }
        }
    
        
    }
}
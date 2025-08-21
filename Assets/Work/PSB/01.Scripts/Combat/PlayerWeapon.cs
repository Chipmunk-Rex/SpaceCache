using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerWeapon : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        [SerializeField] private float angleOffset = 90f;

        private void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - transform.position);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - angleOffset);
        }
    
        
    }
}
using Code.Scripts.Items.Combat;
using UnityEngine;

namespace Work.JJK._01.Scripts
{
    public class LaserDamage : MonoBehaviour
    {
        public float damage = 3f;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.TryGetComponent(out EntityHealth health);
                health.SetHp(-damage);
            }
        }
        
    }
}
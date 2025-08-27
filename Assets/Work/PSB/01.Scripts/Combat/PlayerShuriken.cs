using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerShuriken : MonoBehaviour
    {
        [SerializeField] private float damage = 5f;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Test"))
            {
                other.gameObject.GetComponent<EntityHealth>().SetHp(-damage); 
            }
        }
        
    }
}
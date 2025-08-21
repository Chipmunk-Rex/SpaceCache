using System;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerShield : MonoBehaviour
    {
        public Action OnDestroyAction;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Test"))
            {
                OnDestroyAction?.Invoke();
                Destroy(gameObject);
            }
        }
        
    }
}
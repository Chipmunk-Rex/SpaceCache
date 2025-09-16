using System;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerShield : MonoBehaviour
    {
        public Action OnDestroyAction;
        private int _cnt = 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _cnt++;
            
            if (_cnt <= 20) return;
            
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyBullet") 
                || other.gameObject.layer == LayerMask.NameToLayer("BossBullet")
                || other.gameObject.layer == LayerMask.NameToLayer("Meteor"))
            {
                OnDestroyAction?.Invoke();
                Destroy(gameObject);
            }
        }
        
    }
}
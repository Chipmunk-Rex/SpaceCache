using System;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerShield : MonoBehaviour
    {
        public Action OnDestroyAction;
        public int _cnt = 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _cnt++;
            
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyBullet") 
                || other.gameObject.layer == LayerMask.NameToLayer("BossBullet")
                || other.gameObject.layer == LayerMask.NameToLayer("Meteor"))
            {
                if (_cnt >= 20)
                {
                    OnDestroyAction?.Invoke();
                    Destroy(gameObject);    
                }

                if (other.gameObject.TryGetComponent(out EnemyBullet bullet))
                {
                    bullet.ReturnToPool();
                }

                if (other.gameObject.TryGetComponent(out TorpedoBullet torpedo))
                {
                    torpedo.ReturnToPool();
                }
                
                
                
            }
        }
        
    }
}
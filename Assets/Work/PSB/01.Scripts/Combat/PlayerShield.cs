using System;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerShield : MonoBehaviour
    {
        public Action OnDestroyAction;
        private int cnt = 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            cnt++;
            
            if (cnt <= 5) return;
            
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyBullet") 
                || other.gameObject.layer == LayerMask.NameToLayer("BossBullet")
                || other.gameObject.layer == LayerMask.NameToLayer("Meteor"))
            {
                Debug.Log("ShieldTrigger");
                OnDestroyAction?.Invoke();
                Destroy(gameObject);
            }
        }
        
    }
}
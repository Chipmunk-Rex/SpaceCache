using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerShuriken : MonoBehaviour
    {
        [SerializeField] private float damage = 5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")
                || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                other.gameObject.GetComponent<EntityHealth>().SetHp(-damage); 
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Meteor"))
            {
                UnSuck meteor = other.gameObject.GetComponentInChildren<UnSuck>();
                meteor.currentHP -= damage;
            }
        }
        
    }
}
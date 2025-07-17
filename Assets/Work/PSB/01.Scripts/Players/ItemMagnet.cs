using Code.Scripts.Items;
using UnityEngine;

namespace Code.Scripts.Players
{
    public class ItemMagnet : MonoBehaviour
    {
        [SerializeField] private float radius = 5f;
        [SerializeField] private float destroyDistance = 0.5f;
        [SerializeField] private float pullSpeed = 5f;
        [SerializeField] private LayerMask itemLayer;

        private void FixedUpdate()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, itemLayer);

            foreach (Collider2D hit in hits)
            {
                PickUpItem item = hit.GetComponent<PickUpItem>();
                if (item == null) continue;
                
                Vector3 direction = (transform.position - hit.transform.position).normalized;
                hit.transform.position += direction * (pullSpeed * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, hit.transform.position) < destroyDistance)
                {
                    GetComponent<PlayerLevelSystem>()?.GetManaAtItem();
                    Destroy(hit.gameObject);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    
        
    }
}
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private GameObject dropItemPrefab;
        [SerializeField] private float jumpHeight = 1f;  
        [SerializeField] private float jumpDistance = 1f;
        [SerializeField] private float duration = 0.5f;

        public void Drop()
        {
            GameObject dropItem = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
            
            Vector3 targetPos = transform.position + new Vector3(
                Random.Range(-jumpDistance, jumpDistance), 
                Random.Range(jumpHeight * 0.8f, jumpHeight * 1.2f), 
                0f);

            dropItem.transform.DOMove(targetPos, duration)
                .SetEase(Ease.OutQuad); 
        }
        
    }
}
using DG.Tweening;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Items
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private PoolItemSO dropItemPrefab;
        [SerializeField] private float jumpHeight = 1f;  
        [SerializeField] private float jumpDistance = 1f;
        [SerializeField] private float duration = 0.5f;

        [SerializeField] private int dropValue;
         private bool _hasDropped = false;

         [Inject] private PoolManagerMono _poolManager;
         private PickUpItem dropItem;

         private void Start()
         {
             if (_poolManager == null)
                 _poolManager = FindObjectOfType<PoolManagerMono>();
         }

         public void Drop()
         {
             if (_hasDropped) return;
             _hasDropped = true;
            
             if (_poolManager == null)
             {
                 Debug.LogError("PoolManager is NULL! DropItem cannot spawn items.");
                 return;
             }
             if (dropItemPrefab == null)
             {
                 Debug.LogError("dropItemPrefab is NULL! Assign it in the inspector.");
                 return;
             }
            
             for (int i = 0; i < dropValue; i++)
             {
                 //GameObject dropItem = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
                 dropItem = _poolManager.Pop<PickUpItem>(dropItemPrefab);
                 dropItem.transform.position = transform.position;
            
                 Vector3 targetPos = transform.position + new Vector3(
                     Random.Range(-jumpDistance, jumpDistance), 
                     Random.Range(jumpHeight * 0.8f, jumpHeight * 1.2f), 
                     0f);

                 dropItem.transform.DOMove(targetPos, duration)
                    .SetEase(Ease.OutQuad); 
             }
        }
        
    }
}
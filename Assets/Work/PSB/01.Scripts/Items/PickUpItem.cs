using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class PickUpItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        private Pool _pool;

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            
        }

        public void Push()
        {
            ResetItem();
            _pool.Push(this); 
        }
        
        
    }
}
using System;
using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class PickUpItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        private Pool _pool;

        private float _currentTime;

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= 10f)
            {
                Push();
            }
        }

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            _currentTime = 0f;
        }

        public void Push()
        {
            ResetItem();
            _pool.Push(this); 
        }
        
        
    }
}
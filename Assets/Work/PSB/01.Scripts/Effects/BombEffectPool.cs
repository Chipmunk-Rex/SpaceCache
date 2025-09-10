using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Scripts.Effects
{
    public class BombEffectPool : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        [SerializeField] private Animator animator;
        [SerializeField] private string playStateName = "Play";

        public GameObject GameObject => gameObject;

        private Pool _myPool;

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            if (animator != null)
            {
                animator.Rebind();
                animator.Update(0f); 
            }
            gameObject.SetActive(false);
        }

        public void PlayEffect(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
            gameObject.SetActive(true);

            if (animator != null)
            {
                animator.Play(playStateName, -1, 0f);
            }
        }

        private void OnValidate()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
        }
        
    }
}
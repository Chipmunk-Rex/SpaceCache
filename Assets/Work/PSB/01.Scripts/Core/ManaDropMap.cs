using System.Collections;
using UnityEngine;
using PSB_Lib.ObjectPool.RunTime; 
using PSB_Lib.Dependencies;      
using Random = UnityEngine.Random;

namespace Code.Scripts.Items.Core
{
    public class ManaDropMap : MonoBehaviour
    {
        public Transform player;
        public int maxItems = 30;
        public float spawnRadius = 10f;
        public float minDistanceFromPlayer = 1f;

        public float minSpawnDelay = 0.5f;
        public float maxSpawnDelay = 2f;

        [SerializeField] private PoolItemSO dropItemPrefab; 
        [Inject] private PoolManagerMono _poolManager;

        private int spawnedCount = 0;

        private void Start()
        {
            if (_poolManager == null)
                _poolManager = FindObjectOfType<PoolManagerMono>();
            
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (spawnedCount < maxItems)
            {
                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(delay);

                Vector2 pos = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
                if (Vector2.Distance(pos, player.position) < minDistanceFromPlayer) continue;
                
                var dropItem = _poolManager.Pop<PickUpItem>(dropItemPrefab);
                dropItem.transform.position = pos;

                spawnedCount++;
            }
        }
        
        
    }
}
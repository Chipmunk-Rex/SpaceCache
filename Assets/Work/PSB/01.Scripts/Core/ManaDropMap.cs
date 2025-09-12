using System.Collections;
using UnityEngine;
using PSB_Lib.ObjectPool.RunTime; 
using PSB_Lib.Dependencies;      
using Random = UnityEngine.Random;

namespace Code.Scripts.Items.Core
{
    public class ManaDropMap : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private int maxItems = 30;
        [SerializeField] private float spawnRadius = 10f;
        [SerializeField] private float minDistanceFromPlayer = 1f;

        [SerializeField] private float minSpawnDelay = 0.5f;
        [SerializeField] private float maxSpawnDelay = 2f;

        [SerializeField] private PoolItemSO dropItemPrefab; 
        [Inject] private PoolManagerMono _poolManager;

        private int _spawnedCount = 0;

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (_spawnedCount < maxItems)
            {
                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(delay);

                int spawnBatch = Random.Range(1, 6);

                for (int i = 0; i < spawnBatch && _spawnedCount < maxItems; i++)
                {
                    Vector2 pos = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
                    if (Vector2.Distance(pos, player.position) < minDistanceFromPlayer) continue;

                    var dropItem = _poolManager.Pop<PickUpItem>(dropItemPrefab);
                    dropItem.transform.position = pos;

                    _spawnedCount++;
                }
            }
        }
        
        
    }
}
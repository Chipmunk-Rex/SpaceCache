using System.Collections;
using UnityEngine;

namespace Code.Scripts.Items.Core
{
    public class ManaDropMap : MonoBehaviour
    {
        public Transform player;
        public GameObject itemPrefab;
        public int maxItems = 30;
        public float spawnRadius = 10f;
        public float minDistanceFromPlayer = 1f;

        public float minSpawnDelay = 0.5f;
        public float maxSpawnDelay = 2f;

        private int spawnedCount = 0;

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (spawnedCount < maxItems)
            {
                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(delay);

                
                // 여기서 생성 부분 처리 (랜덤 위치 구하고 Instantiate 호출 등)
                Vector2 pos = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
                if (Vector2.Distance(pos, player.position) < minDistanceFromPlayer) continue;

                //Instantiate(itemPrefab, pos, Quaternion.identity);
                Debug.Log(pos);
                spawnedCount++;
            }
        }
        
        
    }
}
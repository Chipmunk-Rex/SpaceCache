using UnityEngine;

public class EnemySPown : MonoBehaviour
{
    private int enemyPoolSize = 50;
    public GameObject enemyPrefab;
    private GameObject[] enemyPool;

    public float spawnInterval = 1.0f; // 1초마다 적 스폰
    private float timer = 0f;

    void Start()
    {
        enemyPool = new GameObject[enemyPoolSize];
        for (int i = 0; i < enemyPoolSize; i++)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyPool[i].SetActive(false);
        }

        Debug.Log("적 풀 생성 완료");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                enemyPool[i].transform.position = transform.position; // 스폰 위치 초기화
                enemyPool[i].SetActive(true);
                Debug.Log("적 스폰됨: " + i);
                break;
            }
        }
    }
}
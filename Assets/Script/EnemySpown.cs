using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemySPown : MonoBehaviour
{
    private int enemyPoolSize = 50;
    public GameObject enemyPrefab;
    private GameObject[] enemyPool;

    public float spawnInterval = 1.0f;
    private float timer = 0f;

    void Start()
    {
        enemyPool = new GameObject[enemyPoolSize];
        for (int i = 0; i < enemyPoolSize; i++)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyPool[i].SetActive(false);
        }
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
                Vector3 spawnPosition = GetRandomOutsideCameraPosition();
                enemyPool[i].transform.position = spawnPosition;
                enemyPool[i].SetActive(true);
                Debug.Log("스폰함 " + spawnPosition);
                break;
            }
        }
    }

    Vector3 GetRandomOutsideCameraPosition()
    {
        float randomX = Random.Range(40f, -40f);
        float randomY = Random.Range(-40f, 40f);
        return new Vector3(randomX, randomY, 0f);
    }
}
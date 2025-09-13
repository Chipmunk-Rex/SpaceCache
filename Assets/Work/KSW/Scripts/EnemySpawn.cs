using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float _spawnInterval = 2f;
    private void WaveSystem()
    {
        if (_spawnInterval > 0)
        {
            Debug.Log("웨이브 시자");
        }
    }
}
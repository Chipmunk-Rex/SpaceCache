using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private Vector2 mapSize;

    public GameObject Spawn(Vector2 dir)
    {
        int randomIndex = Random.Range(0, mapPrefabs.Length);
        GameObject selectedPrefab = mapPrefabs[randomIndex];
        Vector3 spawnPos = transform.position + new Vector3(dir.x * mapSize.x, dir.y * mapSize.y, 0);
        return Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }
}
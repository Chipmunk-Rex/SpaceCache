using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawner : MonoBehaviour
{
    public GameObject mapPrefab;
    public Vector2 mapSize;
    public GameObject Spawn(Vector2 dir)
    {
        Vector3 spawnPos = transform.position + new Vector3(dir.x * mapSize.x, dir.y * mapSize.y, 0);
        return Instantiate(mapPrefab, spawnPos, Quaternion.identity);
    }
}

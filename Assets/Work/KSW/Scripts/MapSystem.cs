using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private Transform player; 
    [SerializeField] private int MapSize = 128; 
    [SerializeField] private int viewRange = 2; 

    private Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>();

    private void Update()
    {
        int x = Mathf.RoundToInt(player.position.x / MapSize);
        int y = Mathf.RoundToInt(player.position.y / MapSize);

        Vector2Int currentChunk = new Vector2Int(x, y);
        for (int offsetX = -viewRange; offsetX <= viewRange; offsetX++)
        {
            for (int offsetY = -viewRange; offsetY <= viewRange; offsetY++)
            {
                Vector2Int newChunk = new Vector2Int(x + offsetX, y + offsetY);

                if (!spawnedChunks.ContainsKey(newChunk))
                {
                    SpawnChunk(newChunk);
                }
            }
        }

        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var chunk in spawnedChunks)
        {
            int distX = Mathf.Abs(chunk.Key.x - currentChunk.x);
            int distY = Mathf.Abs(chunk.Key.y - currentChunk.y);

            if (distX > viewRange || distY > viewRange)
            {
                Destroy(chunk.Value);
                toRemove.Add(chunk.Key);
            }
        }

        foreach (var key in toRemove)
        {
            spawnedChunks.Remove(key);
        }
    }
    private void SpawnChunk(Vector2Int chunkCoord)
    {
        Vector3 spawnPos = new Vector3(chunkCoord.x * MapSize, chunkCoord.y * MapSize, 0);
        GameObject obj = Instantiate(mapPrefab, spawnPos, Quaternion.identity);
        spawnedChunks.Add(chunkCoord, obj);
    }
}

using System.Collections.Generic;
using UnityEngine;


public class MapSystem : MonoBehaviour
{
    [SerializeField] private GameObject spritePrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int spriteSize = 32;
    [SerializeField] private int viewRange = 2;

    private Dictionary<Vector2Int, GameObject> _activeSprites = new Dictionary<Vector2Int, GameObject>();
    private Queue<GameObject> objectPool = new Queue<GameObject>();
    private Vector2Int lastPlayerIn;

    void Start()
    {
        int poolSize = (viewRange * 2 + 1) * (viewRange * 2 + 1);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject spriteObj = Instantiate(spritePrefab, Vector3.zero, Quaternion.identity, this.transform);
            objectPool.Enqueue(spriteObj);
        }

        lastPlayerIn = _GetMapCoord(player.position);
        UpdateSprites();
    }

    void LateUpdate()
    {
        Vector2Int _currentPlayerMapCoord = _GetMapCoord(player.position);
        if (_currentPlayerMapCoord != lastPlayerIn)
        {
            lastPlayerIn = _currentPlayerMapCoord;
            UpdateSprites();
        }
    }

    void UpdateSprites()
    {
        List<Vector2Int> toDeactivate = new List<Vector2Int>();
        foreach (var chunk in _activeSprites)
        {
            if (Mathf.Abs(chunk.Key.x - lastPlayerIn.x) > viewRange ||
                Mathf.Abs(chunk.Key.y - lastPlayerIn.y) > viewRange)
            {
                toDeactivate.Add(chunk.Key);
            }
        }

        foreach (var coord in toDeactivate)
        {
            GameObject spriteToPool = _activeSprites[coord];
            objectPool.Enqueue(spriteToPool);
            _activeSprites.Remove(coord);
        }

        for (int xOffset = -viewRange; xOffset <= viewRange; xOffset++)
        {
            for (int yOffset = -viewRange; yOffset <= viewRange; yOffset++)
            {
                Vector2Int newCoord = new Vector2Int(lastPlayerIn.x + xOffset, lastPlayerIn.y + yOffset);
                if (!_activeSprites.ContainsKey(newCoord))
                {
                    PositionSprite(newCoord);
                }
            }
        }
    }

    void PositionSprite(Vector2Int coord)
    {
        if (objectPool.Count == 0) return;

        GameObject spriteObj = objectPool.Dequeue();
        spriteObj.transform.position = new Vector3(coord.x * spriteSize, coord.y * spriteSize, 0);
        spriteObj.name = $"Sprite {coord.x}, {coord.y}";
        _activeSprites.Add(coord, spriteObj);
    }

    private Vector2Int _GetMapCoord(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x + 0.01f) / spriteSize);
        int y = Mathf.FloorToInt((position.y + 0.01f) / spriteSize);
        return new Vector2Int(x, y);
    }
}

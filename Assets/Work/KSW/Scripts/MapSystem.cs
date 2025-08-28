using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int MapSize = 32;
    [SerializeField] private int viewRange = 2;

    private Dictionary<Vector2Int, GameObject> _activeMaps = new Dictionary<Vector2Int, GameObject>();
    private Queue<GameObject> ObjectPool = new Queue<GameObject>();
    private Vector2Int lastPlayerIn;

    void Start()
    {
        int poolSize = (viewRange * 2 + 1) * (viewRange * 2 + 1);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject Map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, this.transform);
            ObjectPool.Enqueue(Map); // 비활성화 안 함
        }

        lastPlayerIn = _GetMapCoord(player.position);
        UpdateMaps();
    }

    void LateUpdate()
    {
        Vector2Int _currentPlayerMapCoord = _GetMapCoord(player.position);
        if (_currentPlayerMapCoord != lastPlayerIn)
        {
            lastPlayerIn = _currentPlayerMapCoord;
            UpdateMaps();
        }
    }

    void UpdateMaps()
    {
        List<Vector2Int> _mapToDeactivate = new List<Vector2Int>();
        foreach (var chunk in _activeMaps)
        {
            if (Mathf.Abs(chunk.Key.x - lastPlayerIn.x) > viewRange ||
                Mathf.Abs(chunk.Key.y - lastPlayerIn.y) > viewRange)
            {
                _mapToDeactivate.Add(chunk.Key);
            }
        }

        // 비활성화하지 않고 단순히 Pool로 돌려보냄
        foreach (var coord in _mapToDeactivate)
        {
            GameObject chunkToPool = _activeMaps[coord];
            ObjectPool.Enqueue(chunkToPool);
            _activeMaps.Remove(coord);
        }

        // 필요한 위치에 맵 배치
        for (int xOffset = -viewRange; xOffset <= viewRange; xOffset++)
        {
            for (int yOffset = -viewRange; yOffset <= viewRange; yOffset++)
            {
                Vector2Int newMapCoord = new Vector2Int(lastPlayerIn.x + xOffset, lastPlayerIn.y + yOffset);
                if (!_activeMaps.ContainsKey(newMapCoord))
                {
                    _PositionMap(newMapCoord);
                }
            }
        }
    }

    void _PositionMap(Vector2Int _mapCoord)
    {
        if (ObjectPool.Count == 0) return;

        GameObject _Map = ObjectPool.Dequeue();
        _Map.transform.position = new Vector3(_mapCoord.x * MapSize, _mapCoord.y * MapSize, 0);
        _Map.name = $"Chunk {_mapCoord.x}, {_mapCoord.y}";
        _activeMaps.Add(_mapCoord, _Map);
    }

    // 좌표 계산을 안정화 (경계에서 깜빡임 방지용 오프셋)
    private Vector2Int _GetMapCoord(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x + 0.01f) / MapSize);
        int y = Mathf.FloorToInt((position.y + 0.01f) / MapSize);
        return new Vector2Int(x, y);
    }
}

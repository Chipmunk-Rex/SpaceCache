using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSystem : MonoBehaviour
{
    [Header("플레이어 및 청크 설정")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("맵 생성 설정")]
    public int chunkSize = 16;
    public int loadDistance = 2;
    public float noiseScale = 0.1f;

    [Header("타일 설정")]
    public TileBase grassTile;
    public TileBase waterTile;

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();
    private Queue<GameObject> chunkPool = new Queue<GameObject>(); // 오브젝트 풀링을 위한 큐
    private Vector2Int playerChunkCoord;

    void Start()
    {
        // 초기 로딩
        UpdateChunks();
    }

    void Update()
    {
        Vector2Int currentPlayerChunkCoord = GetChunkCoordFromPosition(player.position);

        if (currentPlayerChunkCoord != playerChunkCoord)
        {
            playerChunkCoord = currentPlayerChunkCoord;
            UpdateChunks();
        }
    }

    void UpdateChunks()
    {
        UnloadOldChunks();

        for (int xOffset = -loadDistance; xOffset <= loadDistance; xOffset++)
        {
            for (int yOffset = -loadDistance; yOffset <= loadDistance; yOffset++)
            {
                Vector2Int chunkToLoadCoord = new Vector2Int(playerChunkCoord.x + xOffset, playerChunkCoord.y + yOffset);

                // 이미 활성화된 청크가 아니면 로드
                if (!activeChunks.ContainsKey(chunkToLoadCoord))
                {
                    LoadChunk(chunkToLoadCoord);
                }
            }
        }
    }

    void LoadChunk(Vector2Int chunkCoord)
    {
        GameObject chunkInstance;

        // 1. 오브젝트 풀링: 풀에 사용 가능한 청크가 있는지 확인
        if (chunkPool.Count > 0)
        {
            chunkInstance = chunkPool.Dequeue();
            chunkInstance.transform.position = GetChunkPosition(chunkCoord);
            chunkInstance.SetActive(true);
        }
        else
        {
            chunkInstance = Instantiate(chunkPrefab, GetChunkPosition(chunkCoord), Quaternion.identity, transform);
        }

        chunkInstance.name = $"Chunk {chunkCoord.x}, {chunkCoord.y}";
        activeChunks.Add(chunkCoord, chunkInstance);

        // 2. 코루틴으로 맵 생성 실행
        Tilemap tilemap = chunkInstance.GetComponentInChildren<Tilemap>();
        StartCoroutine(GenerateChunkDataCoroutine(tilemap, chunkCoord));
    }

    // 코루틴을 이용한 비동기 청크 생성
    IEnumerator GenerateChunkDataCoroutine(Tilemap tilemap, Vector2Int chunkCoord)
    {
        // SetTilesBlock을 위한 준비
        BoundsInt bounds = new BoundsInt(0, 0, 0, chunkSize, chunkSize, 1);
        TileBase[] tiles = new TileBase[chunkSize * chunkSize];

        // 타일 데이터 계산 (이 부분이 가장 무거운 작업)
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                float globalX = (chunkCoord.x * chunkSize + x) * noiseScale;
                float globalY = (chunkCoord.y * chunkSize + y) * noiseScale;
                float noiseValue = Mathf.PerlinNoise(globalX, globalY);

                tiles[x + y * chunkSize] = (noiseValue > 0.5f) ? grassTile : waterTile;
            }
        }

        // 다음 프레임까지 대기하여 메인 스레드 부담 감소
        yield return null;

        // 3. SetTilesBlock으로 타일 한 번에 설정
        if (tilemap != null) // 청크가 비활성화되지 않았을 때만 타일 설정
        {
            tilemap.SetTilesBlock(bounds, tiles);
        }
    }

    void UnloadOldChunks()
    {
        List<Vector2Int> chunksToUnload = new List<Vector2Int>();
        foreach (var chunk in activeChunks)
        {
            // 플레이어와의 거리가 멀어지면 제거 목록에 추가
            // Distance 대신 SqrMagnitude를 사용하면 제곱근 계산을 피할 수 있어 더 빠릅니다.
            float distance = Vector2.SqrMagnitude(new Vector2(chunk.Key.x, chunk.Key.y) - new Vector2(playerChunkCoord.x, playerChunkCoord.y));
            if (distance > (loadDistance + 1) * (loadDistance + 1))
            {
                chunksToUnload.Add(chunk.Key);
            }
        }

        foreach (var chunkCoord in chunksToUnload)
        {
            GameObject chunkToDeactivate = activeChunks[chunkCoord];
            chunkToDeactivate.SetActive(false); // Destroy 대신 비활성화
            chunkPool.Enqueue(chunkToDeactivate); // 풀에 반환
            activeChunks.Remove(chunkCoord);
        }
    }

    // 유틸리티 함수들 (이전 코드와 동일)
    Vector2Int GetChunkCoordFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize);
        int y = Mathf.FloorToInt(position.y / chunkSize);
        return new Vector2Int(x, y);
    }

    Vector3 GetChunkPosition(Vector2Int chunkCoord)
    {
        return new Vector3(chunkCoord.x * chunkSize, chunkCoord.y * chunkSize, 0);
    }
}

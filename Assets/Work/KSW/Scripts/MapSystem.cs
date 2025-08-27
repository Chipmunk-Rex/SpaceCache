using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSystem : MonoBehaviour
{
    [Header("�÷��̾� �� ûũ ����")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("�� ���� ����")]
    public int chunkSize = 16;
    public int loadDistance = 2;
    public float noiseScale = 0.1f;

    [Header("Ÿ�� ����")]
    public TileBase grassTile;
    public TileBase waterTile;

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();
    private Queue<GameObject> chunkPool = new Queue<GameObject>(); // ������Ʈ Ǯ���� ���� ť
    private Vector2Int playerChunkCoord;

    void Start()
    {
        // �ʱ� �ε�
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

                // �̹� Ȱ��ȭ�� ûũ�� �ƴϸ� �ε�
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

        // 1. ������Ʈ Ǯ��: Ǯ�� ��� ������ ûũ�� �ִ��� Ȯ��
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

        // 2. �ڷ�ƾ���� �� ���� ����
        Tilemap tilemap = chunkInstance.GetComponentInChildren<Tilemap>();
        StartCoroutine(GenerateChunkDataCoroutine(tilemap, chunkCoord));
    }

    // �ڷ�ƾ�� �̿��� �񵿱� ûũ ����
    IEnumerator GenerateChunkDataCoroutine(Tilemap tilemap, Vector2Int chunkCoord)
    {
        // SetTilesBlock�� ���� �غ�
        BoundsInt bounds = new BoundsInt(0, 0, 0, chunkSize, chunkSize, 1);
        TileBase[] tiles = new TileBase[chunkSize * chunkSize];

        // Ÿ�� ������ ��� (�� �κ��� ���� ���ſ� �۾�)
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

        // ���� �����ӱ��� ����Ͽ� ���� ������ �δ� ����
        yield return null;

        // 3. SetTilesBlock���� Ÿ�� �� ���� ����
        if (tilemap != null) // ûũ�� ��Ȱ��ȭ���� �ʾ��� ���� Ÿ�� ����
        {
            tilemap.SetTilesBlock(bounds, tiles);
        }
    }

    void UnloadOldChunks()
    {
        List<Vector2Int> chunksToUnload = new List<Vector2Int>();
        foreach (var chunk in activeChunks)
        {
            // �÷��̾���� �Ÿ��� �־����� ���� ��Ͽ� �߰�
            // Distance ��� SqrMagnitude�� ����ϸ� ������ ����� ���� �� �־� �� �����ϴ�.
            float distance = Vector2.SqrMagnitude(new Vector2(chunk.Key.x, chunk.Key.y) - new Vector2(playerChunkCoord.x, playerChunkCoord.y));
            if (distance > (loadDistance + 1) * (loadDistance + 1))
            {
                chunksToUnload.Add(chunk.Key);
            }
        }

        foreach (var chunkCoord in chunksToUnload)
        {
            GameObject chunkToDeactivate = activeChunks[chunkCoord];
            chunkToDeactivate.SetActive(false); // Destroy ��� ��Ȱ��ȭ
            chunkPool.Enqueue(chunkToDeactivate); // Ǯ�� ��ȯ
            activeChunks.Remove(chunkCoord);
        }
    }

    // ��ƿ��Ƽ �Լ��� (���� �ڵ�� ����)
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

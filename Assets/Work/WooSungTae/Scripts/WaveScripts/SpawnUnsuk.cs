using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnsuk : MonoBehaviour
{
    [SerializeField] private GameObject unSuk;
    [SerializeField] private int maxCount = 10; // enemy spawn max
    [SerializeField] private float _spawnTime = 20; // enemy spawn time
    [SerializeField] private Vector2 spawnRegionSize;
    private Camera cam;
    private Stack<GameObject> stack = new();
    private void Awake()
    {
        Initialize();
        cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        spawnRegionSize = new Vector2(halfWidth, halfHeight);
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    public Vector2 GetRandonSpawnPosition()
    {
        float xOffset = 2f;
        float yOffset = 2f;

        float x = Random.Range(-spawnRegionSize.x - xOffset, spawnRegionSize.x + xOffset);
        float y = Random.Range(-spawnRegionSize.y - yOffset, spawnRegionSize.y + yOffset);

        if (Random.value > 0.5f)
        {
            x = x < 0 ? -spawnRegionSize.x - xOffset : spawnRegionSize.x + xOffset;
        }
        else
        {
            y = y < 0 ? -spawnRegionSize.y - yOffset : spawnRegionSize.y + yOffset;
        }

        return new Vector2(x, y);
    }
    private void Initialize()
    {
        for(int i = 0; i < maxCount; i++)
        {
            GameObject unsuk = Instantiate(unSuk, transform);
            stack.Push(unsuk);
            unsuk.SetActive(false);
        }
    }

    private GameObject UnsukSpawn()
    {
        GameObject unsuk;
        if (stack.Count > 0)
        {
            unsuk = stack.Pop();
        }
        else
        {
            unsuk = Instantiate(unSuk, transform);
        }
        unsuk.transform.position = GetRandonSpawnPosition() + (Vector2)cam.transform.position;
        unsuk.SetActive(true);
        return unsuk;
    }

    private void UnsukDie(GameObject unsuk)
    {
        unsuk.SetActive(false);
        stack.Push(unsuk);
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            UnsukSpawn();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

}

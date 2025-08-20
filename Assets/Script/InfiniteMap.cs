using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private Vector3 spawnOffset;
    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSpawned && collision.CompareTag("Player"))
        {
            Instantiate(mapPrefab, transform.parent.position + spawnOffset, Quaternion.identity);
            hasSpawned = true;
        }
    }
}
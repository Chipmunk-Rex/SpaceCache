using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private GameObject map;      // 생성될 맵 프리팹
    [SerializeField] private Vector3 spawnOffset; // 생성 위치 방향 (Offset)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // deadzone의 위치 + offset 방향으로 생성
            Instantiate(map, transform.position + spawnOffset, Quaternion.identity);
        }
    }
}
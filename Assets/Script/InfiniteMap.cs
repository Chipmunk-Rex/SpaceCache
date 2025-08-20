using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private GameObject map;      // ������ �� ������
    [SerializeField] private Vector3 spawnOffset; // ���� ��ġ ���� (Offset)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // deadzone�� ��ġ + offset �������� ����
            Instantiate(map, transform.position + spawnOffset, Quaternion.identity);
        }
    }
}
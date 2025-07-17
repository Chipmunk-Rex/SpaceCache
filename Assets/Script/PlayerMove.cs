using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
     public float moveSpeed = 5f;
    private Vector2 moveDirection;

    void Update()
    {
        transform.position += (Vector3)(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDir;
    float moveSpeed = 7f;

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Log(moveDir);
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
    }
}

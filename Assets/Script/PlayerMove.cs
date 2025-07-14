using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private float speed = 5f;
    public Vector2 _movedir;


    private void Update()
    {
        transform.position += (Vector3)_movedir * speed * Time.deltaTime;
    }

    private void OnMove(InputValue value)
    {
        _movedir = value.Get<Vector2>();
    }
}

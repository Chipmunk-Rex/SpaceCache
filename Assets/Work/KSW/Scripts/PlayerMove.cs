using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movedir;

    private void FixedUpdate()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            transform.position += Vector3.up * speed * Time.fixedDeltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.position += Vector3.down * speed * Time.fixedDeltaTime;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * speed * Time.fixedDeltaTime;
        }
    }
}
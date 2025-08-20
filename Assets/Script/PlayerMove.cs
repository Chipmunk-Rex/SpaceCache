using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    Vector2 moveDirection;
    private void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            moveDirection += Vector2.up;
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            moveDirection += Vector2.down;
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            moveDirection += Vector2.left;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            moveDirection += Vector2.right;
        }
    }
}
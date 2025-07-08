using System;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class CharacterLookCam : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            LookAtMouse();
        }
        
        private void LookAtMouse()
        {
            if (_mainCamera == null) return;

            Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector3 selfPos = transform.position;
            selfPos.z = 0f;

            Vector2 dir = (mouseWorldPos - selfPos).normalized;

            if (dir.sqrMagnitude > 0.001f)
            {
                transform.up = dir;
            }
        }
        
    }
}
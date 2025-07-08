using System;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField] Rect range;
    private bool iInRange = false;
    private void Update()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (range.Contains(mouseWorldPos))
        {
            iInRange = true;
            Debug.Log("����");
        }
        else
        {
            iInRange = false;
        }
    }

    public bool OnRangeEnter()
    {
        return iInRange;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(range.center, new Vector2(range.width, range.height));
    }
}

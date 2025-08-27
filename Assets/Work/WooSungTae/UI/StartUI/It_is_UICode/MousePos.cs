using System;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField] private Rect range;
    private bool iInRange = false;
    private void Update()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (range.Contains(mouseWorldPos))
        {
            iInRange = true;
            range.x = -4.5f;
            Debug.Log("µé¾î¿È");
        }
        else
        {
            iInRange = false;
            range.x = 5;
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

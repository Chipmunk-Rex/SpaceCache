using UnityEngine;

public class EndLine : MonoBehaviour
{
    public Vector2 direction;
    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSpawned && collision.CompareTag("Player"))
        {
            GameObject newMap = GetComponentInParent<MapSpawner>().Spawn(direction);
            Vector2 oppositeDir = -direction;
            EndLine[] endLines = newMap.GetComponentsInChildren<EndLine>();
            foreach (EndLine end in endLines)
            {
                if (end.direction == oppositeDir)
                {
                    end.gameObject.SetActive(false);
                }
            }
            hasSpawned = true; 
        }
    }
}

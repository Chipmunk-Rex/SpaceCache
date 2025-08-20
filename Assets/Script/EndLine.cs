using UnityEngine;

public class EndLine : MonoBehaviour
{
    [SerializeField] private GameObject map;
    public void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("Player"))
       {
            GameObject maps = Instantiate(map, transform.position, Quaternion.identity);
        }
   }  
}

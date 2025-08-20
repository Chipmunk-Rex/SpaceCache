using UnityEngine;

public class EndLine : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.CompareTag("Player"))
       {
            Debug.Log("플레이어가 엔드라인에 닿음");
            
       }
    }
}

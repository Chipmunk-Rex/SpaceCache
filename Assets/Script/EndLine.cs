using UnityEngine;

public class EndLine : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.CompareTag("Player"))
       {
            Debug.Log("�÷��̾ ������ο� ����");
            
       }
    }
}

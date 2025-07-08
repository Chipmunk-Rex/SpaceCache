using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    Transform playerPos;
    Boss boss;
    Vector3 moveDir;
    float moveSpeed = 15f;
    float lifeTime = 1.5f;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }


    private void OnEnable()
    {
        StartCoroutine(LifeTime());
        moveDir = boss.moveDir;     
    }

    private void FixedUpdate()
    {
        transform.position += moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    private IEnumerator LifeTime()
    {
        Debug.Log("StartLiftime");
        yield return new WaitForSeconds(lifeTime);
        Debug.Log("ELiftime");
        gameObject.SetActive(false);
    }
}

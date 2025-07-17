using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using Unity.Mathematics;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private Vector3 moveDir;
    private float lifeTime = 2f;

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    public void Init(Vector3 dir, float speed)
    {
        moveDir = dir.normalized;
        moveSpeed = speed;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void FixedUpdate()
    {
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}

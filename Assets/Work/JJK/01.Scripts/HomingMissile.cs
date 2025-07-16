using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float homingTime = 2f;
    [SerializeField] private float lifeTime = 5f;

    private Transform target;
    private bool isHoming = true;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        isHoming = true;
        StartCoroutine(HomingDuration());
        StartCoroutine(LifeSpan());
    }

    private void Update()
    {
        if (isHoming && target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.Normalize();

            float rotateAmount = Vector3.Cross(transform.up, dir).z;
            transform.Rotate(0, 0, rotateAmount * rotateSpeed * Time.deltaTime);
        }

        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    private IEnumerator HomingDuration()
    {
        yield return new WaitForSeconds(homingTime);
        isHoming = false;
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
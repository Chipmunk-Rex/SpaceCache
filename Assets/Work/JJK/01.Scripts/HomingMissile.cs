using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float lifeTime = 5f;

    private Transform target;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(LifeSpan());
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(transform.up, dir).z;

        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}

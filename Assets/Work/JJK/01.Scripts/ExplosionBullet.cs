using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    [field: SerializeField] public float damage = 1f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float explodeTime = 2f;
    [SerializeField] private int fragmentCount = 8;
    [SerializeField] private float fragmentSpeed = 7f;

    private Vector3 moveDir;
    private Boss boss;
    private float timeAlive = 0f;

    public void Init(Vector3 dir, float bulletDamage, float speed, Boss bossRef)
    {
        damage = bulletDamage;
        moveDir = dir.normalized;
        moveSpeed = speed;
        boss = bossRef;
        timeAlive = 0f;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        transform.position += moveDir * (moveSpeed * Time.deltaTime);

        if (timeAlive >= explodeTime)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        float angleStep = 360f / fragmentCount;

        for (int i = 0; i < fragmentCount; i++)
        {
            float angle = i * angleStep;
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;

            GameObject fragment = boss.GetPooledBullet();
            if (fragment != null)
            {
                fragment.transform.position = transform.position;
                fragment.SetActive(true);
                fragment.GetComponent<Bullet>().Init(dir, fragmentSpeed, damage);
            }
        }

        gameObject.SetActive(false);
    }
}

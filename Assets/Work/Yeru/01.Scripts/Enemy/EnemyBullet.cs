using UnityEngine;
using System.Collections;
using Code.Scripts.Items.Combat;
using PSB_Lib.StatSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[DisallowMultipleComponent]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float life  = 5f;

    private Rigidbody2D rb;
    private Coroutine lifeCo;
    private float damage;
    
    private EnemyBulletPool _pool;   
    
    public void SetPool(EnemyBulletPool p) => _pool = p;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true; 
    }

    void OnEnable()
    {
        if (lifeCo != null) StopCoroutine(lifeCo);
        lifeCo = StartCoroutine(LifeTimer());
    }

    void OnDisable()
    {
        if (lifeCo != null) { StopCoroutine(lifeCo); lifeCo = null; }
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

 
    public void InitFromMuzzle(Transform muzzle, float damageFromSo)
    {
        transform.position = muzzle.position;
        transform.up = muzzle.up;

        damage = damageFromSo;
        
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        
        rb.linearVelocity = (Vector2)transform.up * speed;
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(life);
        ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var d = other.GetComponentInParent<EntityHealth>();
        if (d != null) d.SetHp(-damage);
        ReturnToPool();
    }
    public void ReturnToPool()
    {
        if (_pool != null) _pool.Return(this);
        else gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using Code.Scripts.Items.Combat;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[DisallowMultipleComponent]
public class TorpedoBullet : MonoBehaviour
{
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform target;        
    [SerializeField] private float turnStart  = 280f;
    [SerializeField] private float turnDuration = 5f; 
    [SerializeField] private float life = 10f;
    
    private float damage;
    
    private float steerT; 
    private Rigidbody2D rb;
    private Coroutine lifeCo;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    void OnEnable()
    {
        if (lifeCo != null) StopCoroutine(lifeCo);
        lifeCo = StartCoroutine(LifeTimer());

        steerT = 0f;

        if (!target)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) target = p.transform;
        }
    }

    void OnDisable()
    {
        if (lifeCo != null) { StopCoroutine(lifeCo); lifeCo = null; }
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        damage = 0f;
    }

    
    public void InitFromMuzzle(Transform muzzle, float damageFromSo)
    {
        transform.position = muzzle.position;
        transform.up = muzzle.up;

        damage = damageFromSo;

        if (!gameObject.activeSelf) gameObject.SetActive(true);
        rb.linearVelocity = (Vector2)transform.up * speed;
        steerT = 0f;
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(life);
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = (Vector2)transform.up * speed;
    
        if (!target) return;
        
        steerT += Time.fixedDeltaTime;
        
        float u = Mathf.Clamp01(steerT / turnDuration); 
        float turnRate = Mathf.Lerp(turnStart, 0f, u);  
        
        if (turnRate > 0f)
        {
            Vector2 to = ((Vector2)target.position - rb.position).normalized;
            float desired = Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.MoveTowardsAngle(rb.rotation, desired, turnRate * Time.fixedDeltaTime);
            rb.MoveRotation(newAngle);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var d = other.GetComponentInParent<EntityHealth>();
        if (d != null) d.SetHp(-damage);
        gameObject.SetActive(false);
    }
}

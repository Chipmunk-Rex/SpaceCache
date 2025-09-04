using UnityEngine;
using System.Collections;
using Code.Scripts.Entities;

public abstract class EnemyBase : Entity, IDamageable
{
    [SerializeField] protected EnemySo data;
    public EnemySo Data => data;

    protected Transform     player;
    protected float           currentHealth;
    protected SpriteRenderer spriteRenderer;
    protected Animator       animator;
    protected Rigidbody2D    rb;
    protected Collider2D     col;

    protected float attackTimer;
    protected bool  isDead; 
    
    public float Health { get; set; }
    public float AttackPower { get; set; } 
    
    public abstract void IncreaseAttack(float amount);   // 공격력 수치 증가
    public abstract void IncreaseDefense(float amount);  // 체력 수치 증가
    
    private ObjectPooling pool;
    
    protected override void Awake()
    {
        base.Awake();
        currentHealth   = data.maxHealth;
        player          = GameObject.FindGameObjectWithTag("Player")?.transform;
        spriteRenderer  = GetComponent<SpriteRenderer>();
        animator        = GetComponent<Animator>();
        rb              = GetComponent<Rigidbody2D>();
        col             = GetComponent<Collider2D>();
        
        pool = GetComponentInParent<ObjectPooling>();

        attackTimer     = 0f;
        OnInit();
    }
    
    protected virtual void OnInit() 
    {
         if (data != null)
         {
             if (data.enemyDamageUp  != 0f) IncreaseAttack ((int)data.enemyDamageUp);
             if (data.enemyDefenseUp != 0f) IncreaseDefense((int)data.enemyDefenseUp);
         }
    }
    
    protected virtual void Move()
    {
        if (player == null || rb == null) return;
        Vector2 dir= (player.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        float newAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, data.rotationSpeed * Time.deltaTime * 8);
        rb.MoveRotation(newAngle);
        float distance = Vector2.Distance(transform.position, player.position);
        rb.linearVelocity  = distance > data.range ? dir * data.moveSpeed : Vector2.zero;
    }
    
    protected abstract void Attack();
    
    public virtual void TakeDamage(float amount)
    {
        if (isDead) return; 
        currentHealth -= Mathf.RoundToInt(amount);
        if (currentHealth <= 0)
            Die();
    }
    
    protected virtual void Die()
    {
        if (isDead) return;      
        isDead = true;
        
        if (rb  != null) { rb.linearVelocity = Vector2.zero; rb.simulated = false; }
        if (col != null) col.enabled = false;

        StartCoroutine(FaidOut());
    }

    private IEnumerator FaidOut()
    {
        animator.SetTrigger("isdead");
        Color color = spriteRenderer.color;
        for (float a = 1f; a > 0f; a -= Time.deltaTime * 1.5f)
        {
            color.a = a;
            spriteRenderer.color = color;
            yield return null; 
        }
        if (pool != null)
                    pool.ReturnEnemy(data, gameObject);
    }

    private void OnEnable()
    {
        isDead = false;
        attackTimer = 0f;

        if (data != null) currentHealth = data.maxHealth;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = true;
        }
        if (col != null) col.enabled = true;

        if (spriteRenderer != null)
        {
            var c = spriteRenderer.color;
            c.a = 1f;            
            spriteRenderer.color = c;
        }
    }
    private void Update()
    {
        if (isDead || currentHealth <= 0) return;
        {
            Move();

            if (player != null)
            {
                float distance = Vector2.Distance(transform.position, player.position);
                
                if (attackTimer > 0f)
                    attackTimer -= Time.deltaTime;
                
                if (distance <= data.engageRange && attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = Mathf.Max(0.01f, data.attackCooldown);
                }
            }
        }
    }
}

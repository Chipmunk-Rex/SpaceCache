using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bomber : EnemyBase
{
    [SerializeField] private float     explosionRadius = 1.7f;  
    [SerializeField] private LayerMask damageLayers;

    private EntityAttack _attackCompo;
    private bool exploded;

    protected override void Awake()
    {
        base.Awake();
        _attackCompo = GetCompo<EntityAttack>();
    }

    protected override void OnInit()
    {
        base.OnInit();
        exploded = false;
    }

    protected override void Attack()
    {
        if (isDead || exploded) return;
        Die();
    }

    public override void IncreaseAttack(float amount)
    {
        _statCompo.IncreaseBaseValue(attackStat, amount);
    }
            
    public override void IncreaseDefense(float amount)
    {
        _statCompo.IncreaseBaseValue(hpStat, amount);
    }

    public override void IncreaseSpeed(float amount)
    {
    }
    
    public void HandleOnHit()
    {
        if (isDead) return;
    }

    public void HandleOnDead()
    {
        if (!exploded)
        {
            exploded = true;

            float dmg = _attackCompo.GetAttack();
            Collider2D[] hits = (damageLayers.value != 0)
                ? Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers)
                : Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (var h in hits)
            {
                if (!h || h.gameObject == gameObject) continue;
                if (h.TryGetComponent<EntityHealth>(out var d))
                    d.SetHp(-dmg);
            }
            
            if (rb) { rb.linearVelocity = Vector2.zero; rb.angularVelocity = 0f; }
            var col = GetComponent<Collider2D>(); if (col) col.enabled = false;
        }

        Die();
    }

   
    protected override void Die()
    {
        if (!exploded)
        {
            HandleOnDead();             // 먼저 폭발 + 내부에서 Die() 재호출
            return;
        }
        base.Die();
    }
    private void OnEnable()
    {
        exploded = false; 
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
    
}
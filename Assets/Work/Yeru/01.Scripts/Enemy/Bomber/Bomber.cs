using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bomber : EnemyBase
{
    [Header("Explosion")]
    [SerializeField] private float     explosionRadius = 1.7f;  // 폭발 반경
    [SerializeField] private float     explosionDamage = 100f;   //폭발 데ㅔ미지
    [SerializeField] private LayerMask damageLayers;            

    private bool exploded;

    protected override void OnInit()
    {
        exploded = false;
    }

    
    protected override void Attack()
    {
        if (currentHealth <= 0) return;
        currentHealth = 0;  
        Die();
    }

   
    protected override void Die()
    {
        if (!exploded)
        {
            exploded = true;

            
            Collider2D[] hits = (damageLayers.value != 0)
                ? Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers)
                : Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (var h in hits)
            {
                if (!h || h.gameObject == gameObject) continue;
                if (h.TryGetComponent<IDamageable>(out var d))
                    d.TakeDamage(explosionDamage);
            }

            
            if (rb) { rb.linearVelocity = Vector2.zero; rb.angularVelocity = 0f; }
            var col = GetComponent<Collider2D>(); if (col) col.enabled = false;
        }

        base.Die(); 
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}
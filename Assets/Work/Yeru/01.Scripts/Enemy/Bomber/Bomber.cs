using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bomber : EnemyBase
{
    [SerializeField] private float     explosionRadius = 1.7f;  // 폭발 반경
    [SerializeField] private float     explosionDamage = 100f;   //폭발 데ㅔ미지
    [SerializeField] private LayerMask damageLayers;
    
    private bool exploded;
    
    private float bonusDamage = 0f;
    private float bonusHealth = 0f;
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnInit()
    {
        base.OnInit();
        exploded = false;
    }

    protected override void Attack()
    {
    }

    public override void IncreaseAttack(float amount)
    {
        bonusDamage += amount;
    }
        
    public override void IncreaseDefense(float amount)
    {
        bonusHealth += amount;
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

            
            Collider2D[] hits = (damageLayers.value != 0)
                ? Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers)
                : Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (var h in hits)
            {
                if (!h || h.gameObject == gameObject) continue;
                if (h.TryGetComponent<EntityHealth>(out var d))
                    d.SetHp(-explosionDamage);
            }
            
            if (rb) { rb.linearVelocity = Vector2.zero; rb.angularVelocity = 0f; }
            var col = GetComponent<Collider2D>(); if (col) col.enabled = false;
        }

        Die();
    }

   
    protected override void Die()
    {
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
using UnityEngine;

public class UnSuckExplosion : MonoBehaviour
{
    [SerializeField] private float Range = 1.5f;
    [SerializeField] private float damege = 100f; 
    [SerializeField] private string playerTag = "Player";

    private float bonusDamage = 0f;
    
    
    private bool _fired; 

    public void IncreaseAttack(float amount)
    {
        bonusDamage += amount;
    }
    
    public void Bob()
    {
        if (_fired) return;
        _fired = true;
        
        float finalDamage = damege + bonusDamage;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Range);
        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag(playerTag)) continue;

            
            if (hit.TryGetComponent(out IDamageable dmg))
            {
                dmg.SetHp(finalDamage);
            }
            else
            {
                Debug.LogWarning($"Player에 IDamageable이 없음: {hit.name}");
            }
        }
    }
    private void OnEnable() => _fired = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
    
}
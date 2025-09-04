using Code.Scripts.Items.Combat;
using UnityEngine;

public class UnSuckExplosion : MonoBehaviour
{
    [SerializeField] private float Range = 1.5f;
    [SerializeField] private float damege = 100f; 
    [SerializeField] private string playerTag = "Player";

    private bool _fired; 

    public void Bob()
    {
        if (_fired) return;
        _fired = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Range);
        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag(playerTag)) continue;

            
            if (hit.TryGetComponent(out EntityHealth dmg))
            {
                dmg.SetHp(-damege);
            }
            else
            {
                Debug.LogWarning($"Player에 IDamageable이 없음: {hit.name}");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
    
}
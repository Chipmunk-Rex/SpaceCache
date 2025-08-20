using UnityEngine;

[DisallowMultipleComponent]
public class PlayerSandbagSimple : MonoBehaviour, IDamageable
{
    [Header("샌드백 설정")]
    [SerializeField] private float maxHp = 1_000_000f; // 엄청 큰 체력
    [SerializeField] private bool  clampToZero = true; // 0 아래로 안내려가게

    private float hp;
    private float totalTaken;
    private int   hitCount;

    private void Awake()
    {
        hp = maxHp;
        totalTaken = 0f;
        hitCount = 0;
    }

    // IDamageable (한 인자만)
    public void TakeDamage(float amount)
    {
        amount = Mathf.Max(0f, amount);

        float before = hp;
        hp -= amount;
        if (clampToZero) hp = Mathf.Max(0f, hp);

        float lost = before - hp;   // 실제 깎인 체력 (힐/클램프 고려)
        totalTaken += lost;
        hitCount++;

        Debug.Log($"[Sandbag] -{lost:F1} dmg  | HP {hp:F1}/{maxHp:F1}  | Total {totalTaken:F1}  | Hits {hitCount}");
    }

    // 필요시 수동 리셋
    public void ResetSandbag()
    {
        hp = maxHp;
        totalTaken = 0f;
        hitCount = 0;
    }
}
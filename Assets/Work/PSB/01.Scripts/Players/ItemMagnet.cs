using Code.Scripts.Entities;
using Code.Scripts.Items;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Players
{
    public class ItemMagnet : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [Header("Magnet")]
        [field : SerializeField] public float radius = 1f;
        [field: SerializeField] public StatSO manaRadiusStat;
        
        [Header("Item")]
        [SerializeField] private float destroyDistance = 1f;
        [SerializeField] private float pullSpeed = 10f;
        [SerializeField] private LayerMask itemLayer;

        private Player _player;
        private EntityStat _statCompo;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            radius = _statCompo.SubscribeStat(manaRadiusStat, HandleManaRadiusChange, 1f);
        }
        
        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(manaRadiusStat, HandleManaRadiusChange);
        }
        
        private void HandleManaRadiusChange(StatSO stat, float currentValue, float prevValue)
        {
            radius = currentValue;
        }
        
        private void FixedUpdate()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, itemLayer);

            foreach (Collider2D hit in hits)
            {
                PickUpItem item = hit.GetComponent<PickUpItem>();
                if (item == null) continue;
                
                Vector3 direction = (transform.position - hit.transform.position).normalized;
                hit.transform.position += direction * (pullSpeed * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, hit.transform.position) < destroyDistance)
                {
                    GetComponent<PlayerLevelSystem>()?.GetManaAtItem();
                    Destroy(hit.gameObject);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
        
    }
}
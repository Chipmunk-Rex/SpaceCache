using Code.Scripts.Entities;
using Code.Scripts.Items.Combat;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Scripts.Player.States
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private PoolItemSO bullet;
        [SerializeField] private Transform spawnPoint;
        
        [Inject] private PoolManagerMono _poolManager;
        
        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        public async void InitialCompo()
        {
            PlayerBullet playerBullet = _poolManager.Pop<PlayerBullet>(bullet);
            
            playerBullet.transform.position = spawnPoint.position;
            playerBullet.transform.rotation = spawnPoint.rotation;
            
            await Awaitable.WaitForSecondsAsync(1);
            _poolManager.Push(playerBullet);
        }
        
        
    }
}
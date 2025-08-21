using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class MagnetRangeVisual : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform rangeVisual;
     
        private Player _player;
        private ItemMagnet _magnet;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _magnet = entity.GetCompo<ItemMagnet>();
        }
        
        private void Update()
        {
            if (_magnet == null || rangeVisual == null) return;

            // 이미지 크기를 반경에 맞게 조절
            float radius = _magnet.radius;
            rangeVisual.localScale = new Vector3(radius, radius, 1f);
        }
        
        
    }
}
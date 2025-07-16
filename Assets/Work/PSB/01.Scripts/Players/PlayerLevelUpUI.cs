using System;
using Code.Scripts.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Players
{
    public class PlayerLevelUpUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameObject[] skillSelectUI;
        
        private Entity _entity;
        private PlayerLevelSystem _levelSystem;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _levelSystem = entity.GetCompo<PlayerLevelSystem>();
        }

        private void OnEnable()
        {
            _levelSystem.OnLevelUp += HandleLevelUpUIShow;
        }

        private void OnDestroy()
        {
            _levelSystem.OnLevelUp -= HandleLevelUpUIShow;
        }

        private void HandleLevelUpUIShow()
        {
            for (int i = 0; i < 3; i++)
            {
                int r = Random.Range(0, skillSelectUI.Length);
                skillSelectUI[r].SetActive(true);
            }
        }
        
        
    }
}
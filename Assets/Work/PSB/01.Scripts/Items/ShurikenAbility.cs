using System.Collections.Generic;
using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class ShurikenAbility : MonoBehaviour, IEntityComponent
    {
        [Header("Settings")] 
        [SerializeField] private GameObject shurikenPrefab;
        [field: SerializeField] public int currentCount = 0;
        [field: SerializeField] public int maxCount = 5;
        
        [Header("Value")]
        [SerializeField] private float radius = 3f;
        [SerializeField] private float orbitSpeed = 100f;   
        [SerializeField] private float selfRotateSpeed = 360f;
        [SerializeField] private float increaseRadius = 0.5f;
        
        private List<Transform> _shurikenList = new List<Transform>();
        private List<Vector3> _originalScales = new List<Vector3>();
        private float _currentAngle = 0f;

        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }
        
        public void UpgradeShuriken()
        {
            if (_shurikenList.Count >= maxCount)
            {
                return;
            }

            currentCount++;
            radius += increaseRadius;
            
            GameObject newShuriken = Instantiate(shurikenPrefab, transform);
            _shurikenList.Add(newShuriken.transform);
            _originalScales.Add(newShuriken.transform.localScale);

            float scaleFactor = 1f + (currentCount - 1) * 0.2f;
            for (int i = 0; i < _shurikenList.Count; i++)
            {
                _shurikenList[i].localScale = _originalScales[i] * scaleFactor;
            }
            
            ArrangeShurikens();
        }
        
        private void Update()
        {
            if (_shurikenList.Count == 0) return;
            
            _currentAngle += orbitSpeed * Time.deltaTime;
            UpdateShurikenPositions();
        }

        private void LateUpdate()
        {
            foreach (var shuriken in _shurikenList)
            {
                shuriken.rotation = Quaternion.identity;
                shuriken.Rotate(Vector3.forward * (selfRotateSpeed * Time.deltaTime));
            }
        }

        private void UpdateShurikenPositions()
        {
            int count = _shurikenList.Count;

            for (int i = 0; i < count; i++)
            {
                float angle = (360f / count) * i + _currentAngle;
                Vector3 offset = new Vector3
                (
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad),
                    0
                ) * radius;

                _shurikenList[i].position = _player.transform.position + offset;
            }
        }

        private void ArrangeShurikens()
        {
            int count = _shurikenList.Count;

            for (int i = 0; i < count; i++)
            {
                float angle = (360f / count) * i;
                Vector3 offset = new Vector3
                (
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad),
                    0
                ) * radius;

                _shurikenList[i].position = _player.transform.position + offset;
            }
        }
        
        
    }
}

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
        [SerializeField] private float radius = 2f;
        [SerializeField] private float orbitSpeed = 100f;   
        [SerializeField] private float selfRotateSpeed = 360f; 
        
        private List<Transform> shurikenList = new List<Transform>();
        private float currentAngle = 0f;

        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }
        
        public void UpgradeShuriken()
        {
            if (shurikenList.Count >= maxCount)
            {
                Debug.Log("표창이 이미 최대치입니다!");
                return;
            }

            currentCount++;
            radius += 0.5f;
            GameObject newShuriken = Instantiate(shurikenPrefab, transform);
            shurikenList.Add(newShuriken.transform);
            
            float scaleFactor = 1f + (currentCount - 1) * 0.2f;
            foreach (var shuriken in shurikenList)
            {
                shuriken.localScale = Vector3.one * scaleFactor;
            }
            
            ArrangeShurikens();
        }
        
        private void Update()
        {
            if (shurikenList.Count == 0) return;
            
            currentAngle += orbitSpeed * Time.deltaTime;
            UpdateShurikenPositions();
        }

        private void LateUpdate()
        {
            foreach (var shuriken in shurikenList)
            {
                shuriken.rotation = Quaternion.identity;
                shuriken.Rotate(Vector3.forward * selfRotateSpeed * Time.deltaTime);
            }
        }

        private void UpdateShurikenPositions()
        {
            int count = shurikenList.Count;

            for (int i = 0; i < count; i++)
            {
                float angle = (360f / count) * i + currentAngle;
                Vector3 offset = new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad),
                    0
                ) * radius;

                // 플레이어 위치 + 궤도 위치
                shurikenList[i].position = _player.transform.position + offset;
            }
        }

        private void ArrangeShurikens()
        {
            // 처음 생성할 때 균등 배치
            int count = shurikenList.Count;

            for (int i = 0; i < count; i++)
            {
                float angle = (360f / count) * i;
                Vector3 offset = new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad),
                    0
                ) * radius;

                shurikenList[i].position = _player.transform.position + offset;
            }
        }


        
    }
}
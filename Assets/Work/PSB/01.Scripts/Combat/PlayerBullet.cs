﻿using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Scripts.Items.Combat
{
    public class PlayerBullet : MonoBehaviour, IPoolable
    {
        [field : SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;
        
        private Pool _myPool;
        private Rigidbody2D _rigidCompo;

        [SerializeField] private float damage = 10f;
        [SerializeField] private float speed = 10f; 
        [SerializeField] private float lifeTime = 3f;

        private float _timer;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
            _rigidCompo = gameObject.GetComponent<Rigidbody2D>();
        }

        public void SetDamage(float dam)
        {
            damage = dam;
        }
        
        private void FixedUpdate()
        {
            MoveForward();
        }

        private void OnEnable()
        {
            _timer = Time.deltaTime;
            if (_timer >= lifeTime)
            {
                _myPool.Pop();
            }
        }

        private void MoveForward()
        {
            _rigidCompo.linearVelocity = transform.up * speed;
        }

        public void ResetItem()
        {
            _rigidCompo.linearVelocity = Vector2.zero;
            _timer = 0f;
        }
        
        #region Temp

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Test"))
            {
                other.gameObject.GetComponent<EntityHealth>().SetHp(-damage); 
                gameObject.SetActive(false);
            }
        }
        
        #endregion
        
    }
}
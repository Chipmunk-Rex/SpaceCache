using System;
using Code.Scripts.Items.Combat;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class HealthStatUI : StatUIItem
    {
        [SerializeField] private EntityHealth health;

        private void Update()
        {
            UpdateValue($"{health.currentHealth} / {health.maxHealth}");    
        }
        
    }
}
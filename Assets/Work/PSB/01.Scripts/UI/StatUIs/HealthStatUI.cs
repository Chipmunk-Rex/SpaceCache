using Code.Scripts.Items.Combat;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class HealthStatUI : StatUIItem
    {
        [SerializeField] private EntityHealth health;

        private void Update()
        {
            UpdateValue($"{(int)health.currentHealth} / {(int)health.maxHealth}");
        }
        
    }
}
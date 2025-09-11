using System.Collections.Generic;
using PSB_Lib.StatSystem;
using UnityEngine;

namespace Code.Scripts.Players
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private List<StatSO> stats = new();
        public IReadOnlyList<StatSO> Stats => stats;

        private void Awake()
        {
            for (int i = 0; i < stats.Count; i++)
            {
                stats[i] = Instantiate(stats[i]);
            }
        }
        
        
    }
}
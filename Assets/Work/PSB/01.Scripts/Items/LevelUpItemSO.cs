using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Items
{
    [CreateAssetMenu(fileName = "LevelUpItem", menuName = "SO/Item", order = 0)]
    public class LevelUpItemSO : ScriptableObject
    {
        public Sprite SkillIcon;
        public string Name;
        [TextArea]
        public string Description;
        
        [NonSerialized]
        public int selectCount;

        [Header("Max")] 
        public int maxCount = 5;
        public bool IsMaxed => selectCount >= maxCount;
        
    }
}
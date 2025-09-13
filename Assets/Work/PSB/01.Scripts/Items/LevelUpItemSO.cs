using System;
using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items
{
    [Flags]
    public enum ItemType
    {
        PASSIVE = 1,
        ACTIVE = 2,
        QCLICK = 4,
        ECLICK = 8,
        RCLICK = 16,
    }
    
    public abstract class LevelUpItemSO : ScriptableObject
    {
        public ItemType itemType;
        public Sprite SkillIcon;
        public string Name;
        [TextArea]
        public string Description;
        
        [NonSerialized]
        public int selectCount;
        public bool cardUiSpawn = false;

        [Header("Max")] 
        public int maxCount = 5;
        public bool IsMaxed => selectCount >= maxCount;
        
        public abstract void ApplyItem(Entity targetEntity);
        
    }
}
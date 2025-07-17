using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class SpeedLevelUpItem : LevelUpItem
    {
        public override void ApplyItem(Entity targetEntity)
        {
            Debug.Log("SpeedItemSelected!!");
        }
        
    }
}
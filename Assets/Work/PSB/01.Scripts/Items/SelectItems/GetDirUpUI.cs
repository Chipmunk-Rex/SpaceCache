using Code.Scripts.Entities;
using UnityEngine;

namespace Code.Scripts.Items
{
    public class GetDirUpUI : LevelUpItem
    {
        public override void ApplyItem(Entity targetEntity)
        {
            Debug.Log("ManaGetDirUpSelected!!");
            _levelUpItemSO.selectCount++;
        }
        
    }
}
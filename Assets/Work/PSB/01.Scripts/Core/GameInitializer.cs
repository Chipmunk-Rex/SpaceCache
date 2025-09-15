using UnityEngine;

namespace Code.Scripts.Items.Core
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private LevelUpItemSO[] levelUpItems;

        private void Awake()
        {
            foreach (var item in levelUpItems)
            {
                item.selectCount = 0;
            }
        }
        
    }
}
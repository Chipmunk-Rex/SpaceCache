using System.Collections.Generic;
using Code.Scripts.Items.UI;
using PSB_Lib.Dependencies;
using UnityEngine;
using System;

namespace Code.Scripts.Items.Core
{
    [Provide]
    public class ItemSOCountManager : MonoBehaviour, IDependencyProvider
    {
        [Serializable]
        public class ItemUIPrefabData
        {
            public LevelUpItemSO itemSo;
            public ItemSOCountUI prefab;
        }

        [SerializeField] private Transform parentUIRoot;
        [SerializeField] private List<ItemUIPrefabData> itemUIPrefabs;

        private Dictionary<LevelUpItemSO, ItemSOCountUI> _activeUIs = new();

        public void ShowOrUpdateUI(LevelUpItemSO so)
        {
            if (!_activeUIs.ContainsKey(so))
            {
                var prefabData = itemUIPrefabs.Find(p => p.itemSo == so);
                if (prefabData == null)
                {
                    Debug.LogWarning($"No prefab assigned for {so.name}");
                    return;
                }
                var instance = Instantiate(prefabData.prefab, parentUIRoot);
                instance.SetItemSO(so);
                _activeUIs[so] = instance;
            }
            _activeUIs[so].ChangeSelectValue();
        }

    }
}
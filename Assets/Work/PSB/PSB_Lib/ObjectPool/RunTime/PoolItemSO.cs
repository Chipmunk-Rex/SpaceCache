using UnityEngine;

namespace PSB_Lib.ObjectPool.RunTime
{
    [CreateAssetMenu(fileName = "PoolItem", menuName = "SO/Pool/Item", order = 0)]
    public class PoolItemSO : ScriptableObject
    {
        public string poolingName;
        public GameObject prefab;
        public int initCount;

    }
}
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Dictionary<string,GameObject> enemyDic;
    private List<EnemySo> enemyList = new();

    private void Start()
    {
        foreach(var enemy in enemyList)
        {
        }
    }

}

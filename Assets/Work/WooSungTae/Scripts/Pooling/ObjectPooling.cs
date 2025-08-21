using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private int enemySpawnMaxCount; // ¿¡³Ê¹Ì ÃÑ ¼ÒÈ¯ ÇÑµµ È½¼ö

    private Dictionary<EnemySo,Stack<GameObject>> enemyDic = new();
    [SerializeField] private List<EnemySo> enemyList = new();

    private void Start()
    {
        EnemyInitialize();
    }

    private void EnemyInitialize()
    {
        foreach (var enemy in enemyList)
        {
            Stack<GameObject> saveEnemy = new();
            for(int i = 0; i < enemySpawnMaxCount; i++)
            {
                GameObject a = Instantiate(enemy.prefab, transform);
                a.SetActive(false);
                saveEnemy.Push(a);
            }
            enemyDic.Add(enemy, saveEnemy);
        }
    }

    public GameObject SpawnEnemy(EnemySo so, Transform spawnPosition)
    {
        if (!enemyDic.ContainsKey(so)) return null;
        GameObject a;
        if (enemyDic[so].Count > 0)
        {
            a = enemyDic[so].Pop();
        }
        else
        {
            a = Instantiate(so.prefab, transform);
        }
        a.SetActive(true);
        a.transform.position = spawnPosition.position;

        return a;
    }

    public void ReturnEnemy(EnemySo so, GameObject enemy)
    {
         enemy.SetActive(false);
         enemyDic[so].Push(enemy);
    }
}

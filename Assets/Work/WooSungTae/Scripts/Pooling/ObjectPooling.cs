using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private int enemySpawnMaxCount; // ¿¡³Ê¹Ì ÃÑ ¼ÒÈ¯ ÇÑµµ È½¼ö
    [SerializeField] private int bossSpawnMaxCount; // º¸½º ÃÑ ¼ÒÈ¯ ÇÑµµ È½¼ö

    private Dictionary<EnemySo,Stack<GameObject>> enemyDic = new();
    [SerializeField] private List<EnemySo> enemyList = new();
    private Dictionary<BossStatSO, Stack<GameObject>> bossDic = new();
    [SerializeField] private List<BossStatSO> bossList = new();

    private void Awake()
    {
        EnemyInitialize();
        BossInitialize();
    }
    #region boss
    private void BossInitialize()
    {
        foreach(var boss in bossList)
        {
            Stack<GameObject> saveBoss = new();
            for(int i = 0; i < bossSpawnMaxCount; i++)
            {
                GameObject boss2 = Instantiate(boss.bossPrefab, transform);
                boss2.SetActive(false);
                saveBoss.Push(boss2);
            }
            bossDic.Add(boss, saveBoss);
        }
    }
    public GameObject SpawnBoss(BossStatSO so, Vector2 spawnPosition)
    {
        if (!bossDic.ContainsKey(so)) return null;
        GameObject boss;
        if (bossDic[so].Count > 0)
        {
            boss = bossDic[so].Pop();
        }
        else
        {
            boss = Instantiate(so.bossPrefab, transform);
        }
        boss.transform.position = new Vector2(spawnPosition.x, spawnPosition.y);
        boss.SetActive(true);

        return boss;
    }

    public void ReturnBoss(BossStatSO so, GameObject boss)
    {
        boss.SetActive(false);
        bossDic[so].Push(boss);
    }
    #endregion
    #region enemy
    private void EnemyInitialize()
    {
        foreach (var enemy in enemyList)
        {
            Stack<GameObject> saveEnemy = new();
            for (int i = 0; i < enemySpawnMaxCount; i++)
            {

                GameObject enemy2 = Instantiate(enemy.prefab, transform);
                enemy2.SetActive(false);
                saveEnemy.Push(enemy2);
            }
            enemyDic.Add(enemy, saveEnemy);
        }
    }

    public GameObject SpawnEnemy(EnemySo so, Vector2 spawnPosition)
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
        a.transform.position = new Vector2(spawnPosition.x, spawnPosition.y);
        a.SetActive(true);

        return a;
    }

    public void ReturnEnemy(EnemySo so, GameObject enemy)
    {
         enemy.SetActive(false);
         enemyDic[so].Push(enemy);
    }
    #endregion
}

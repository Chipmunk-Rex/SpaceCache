using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveListSO waveListSO;
    [SerializeField] private ObjectPooling pooling;
    [SerializeField] private Transform trans;
    private int waveNum = 0;

    private void Start()
    {
        StartCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        foreach(var waveList in waveListSO.waves)
        {
            waveNum++;
            for(int i = 0; i < waveList.enemyCount; i++)
            {
                int a = Random.Range(0, waveList.enemyType.Length);
                pooling.SpawnEnemy(waveList.enemyType[a], trans);
                EnemySo enemyData = waveList.enemyType[a];
                yield return new WaitForSeconds(waveList.enemySpawnTime);
            }
            if(waveNum % 5 == 0)
            {
                for (int i = 0; i < waveList.enemyType.Length; i++)
                {
                    waveList.enemyType[i].damage += waveList.enemyType[i].enemyDamageUp;
                    //waveList.enemyType[i].maxHealth += waveList.enemyType[i].enemyDamageUp;
                }
            }
            else if (waveNum % 3 == 0)
            {
                for (int i = 0; i < waveList.enemyType.Length; i++)
                {
                    waveList.enemyType[i].damage -= waveList.enemyType[i].enemyDamageUp;
                    //waveList.enemyType[i].maxHealth -= waveList.enemyType[i].enemyDamageUp;
                }
            }
            else
            {
                for (int i = 0; i < waveList.enemyType.Length; i++)
                {
                    waveList.enemyType[i].damage += waveList.enemyType[i].enemyDamageUp;
                    //waveList.enemyType[i].maxHealth += waveList.enemyType[i].enemyDamageUp;
                }
            }
            yield return new WaitForSeconds(waveList.waveEndTime);
        }
    }
}

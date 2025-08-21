using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveListSO waveListSO;
    [SerializeField] private ObjectPooling pooling;
    [SerializeField] private Transform trans;

    private void Start()
    {
        StartCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        foreach(var waveList in waveListSO.waves)
        {
            for(int i = 0; i < waveList.enemyCount; i++)
            {
                pooling.SpawnEnemy(waveList.enemyType[0], trans);
                yield return new WaitForSeconds(waveList.enemySpawnTime);
            }
            yield return new WaitForSeconds(waveList.waveEndTime);
        }
    }
}

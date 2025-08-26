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
                if (waveNum % 5 == 0)
                {
                    //나중에 적 스크립트에 공격력, 방어력 있으면 그 것 가져와서 공격력 강화시키기 수정하기
                }
                else if (waveNum % 3 == 0)
                {
                    //나중에 적 스크립트에 공격력, 방어력 있으면 그 것 가져와서 공격력 강화시키기 수정하기
                }
                else
                {
                    //나중에 적 스크립트에 공격력, 방어력 있으면 그 것 가져와서 공격력 강화시키기 수정하기
                }
                yield return new WaitForSeconds(waveList.enemySpawnTime);
            }
            yield return new WaitForSeconds(waveList.waveEndTime);
        }
    }
}

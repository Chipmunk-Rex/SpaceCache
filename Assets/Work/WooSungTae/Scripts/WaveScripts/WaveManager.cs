using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveListSO waveListSO;
    [SerializeField] private ObjectPooling pooling;
    [SerializeField] private Vector2 spawnRegionSize;
    [SerializeField] private TextMeshProUGUI _text;
    private Camera cam;
    private int waveNum = 0;

    private void Awake()
    {
        cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        spawnRegionSize = new Vector2(halfWidth, halfHeight);
    }
    private void Start()
    {
        StartCoroutine(Wave());
        _text.text = $"wave : {waveNum}";
    }
    public Vector2 GetRandonSpawnPosition()
    {
        float xOffset = 2f;
        float yOffset = 2f;

        float x = Random.Range(-spawnRegionSize.x - xOffset, spawnRegionSize.x + xOffset);
        float y = Random.Range(-spawnRegionSize.y - yOffset, spawnRegionSize.y + yOffset);

        if (Random.value > 0.5f)
        {
            x = x < 0 ? -spawnRegionSize.x - xOffset : spawnRegionSize.x + xOffset;
        }
        else
        {
            y = y < 0 ? -spawnRegionSize.y - yOffset : spawnRegionSize.y + yOffset;
        }

        return new Vector2(x, y);
    }

    IEnumerator Wave()
    {
        foreach(var waveList in waveListSO.waves)
        {
            waveNum++;
            _text.text = $"wave : {waveNum}";

            if(waveList.bossSpawn)
            {
                StartCoroutine(BossEnter(waveList));
            }
            foreach(var e in waveList.sequence)
            {
                for(int i = 0; i < e.count; i++)
                {
                    GameObject enemy = pooling.SpawnEnemy(e.enemy, GetRandonSpawnPosition() + (Vector2)cam.transform.position);
                    yield return new WaitForSeconds(e.defaultGap);
                    UpgradeEnemy(enemy, e.enemy);
                }
            }
            yield return new WaitForSeconds(waveList.waveEndTime);
        }
    }
    IEnumerator BossEnter(WaveSO waveSO)
    {
        float time = 0;
        while (waveSO.bossSpawnTiming > time)
        {
            time += Time.deltaTime;
            yield return null;
        }
        pooling.SpawnBoss(waveSO.boss, GetRandonSpawnPosition() + (Vector2)cam.transform.position);
    }

    public void UpgradeEnemy(GameObject enemy, EnemySo so)
    {
        if(waveNum > 12)
        {
            if (enemy.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
            {
                enemyBase.IncreaseAttack(so.enemyDamageUp * 4);
                enemyBase.IncreaseDefense(so.enemyDefenseUp * 4);
            }
        }
        else if(waveNum > 9)
        {
            if (enemy.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
            {
                enemyBase.IncreaseAttack(so.enemyDamageUp * 3);
                enemyBase.IncreaseDefense(so.enemyDefenseUp * 3);
            }
        }
        else if(waveNum > 6)
        {
            if (enemy.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
            {
                enemyBase.IncreaseAttack(so.enemyDamageUp * 2);
                enemyBase.IncreaseDefense(so.enemyDefenseUp * 2);
            }
        }
        else if(waveNum > 3)
        {
            if (enemy.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
            {
                enemyBase.IncreaseAttack(so.enemyDamageUp * 1.5f);
                enemyBase.IncreaseDefense(so.enemyDefenseUp * 1.5f);
            }
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveListSO waveListSO;
    [SerializeField] private ObjectPooling pooling;
    [SerializeField] private Vector2 spawnRegionSize;
    [SerializeField] private TextMeshProUGUI _text;
    public UnityEvent onWaveEnd;
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
        StartCoroutine(WaveRoutine());
    }

    private Vector2 GetRandonSpawnPosition()
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

    private IEnumerator WaveRoutine()
    {
        foreach (var waveList in waveListSO.waves)
        {
            // 1. 웨이브 시작 전 대기 3초 (3,2,1 카운트)
            yield return StartCoroutine(PreWaveCountdown());

            // 2. 웨이브 번호 표시
            waveNum++;
            _text.text = $"Wave : {waveNum}";
            _text.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f); // 3초 동안 텍스트 표시
            _text.gameObject.SetActive(false);

            // 3. 보스 스폰 코루틴 시작
            if (waveList.bossSpawn)
            {
                StartCoroutine(BossEnter(waveList));
            }

            // 4. 일반 적 스폰
            foreach (var e in waveList.sequence)
            {
                for (int i = 0; i < e.count; i++)
                {
                    GameObject enemy = pooling.SpawnEnemy(e.enemy, GetRandonSpawnPosition() + (Vector2)cam.transform.position);
                    if (waveNum >= 3)
                    {
                        Debug.Log("UpgradeEnemy");
                        UpgradeEnemy(enemy, e.enemy);
                    }
                    yield return new WaitForSeconds(e.defaultGap);
                }
            }

            // 5. 웨이브 종료 대기
            float a = 0;
            while (a < waveList.waveEndTime && pooling.enemyCount > 0)
            {
                a += Time.deltaTime;
                yield return null;
            }
        }

        onWaveEnd?.Invoke();
    }

    private IEnumerator PreWaveCountdown()
    {
        _text.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        _text.gameObject.SetActive(false);
    }

    private IEnumerator BossEnter(WaveSO waveSO)
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
        if (!enemy.TryGetComponent<EnemyBase>(out EnemyBase enemyBase)) return;

        float mul =
            (waveNum > 12) ? 4f :
            (waveNum > 9) ? 3f :
            (waveNum > 6) ? 2f :
            (waveNum > 3) ? 1.5f : 1f;

        enemyBase.IncreaseAttack(so.enemyDamageUp * mul);
        enemyBase.IncreaseDefense(so.enemyDefenseUp * mul);
        enemyBase.IncreaseSpeed(so.enemySpeedUp);
    }
    
    
}

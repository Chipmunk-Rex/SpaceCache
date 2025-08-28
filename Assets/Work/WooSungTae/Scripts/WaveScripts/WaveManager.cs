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
            for (int i = 0; i < waveList.enemyCount; i++)
            {
                int enemyType = Random.Range(0, waveList.enemyType.Length);
                GameObject enemy = pooling.SpawnEnemy(waveList.enemyType[enemyType], GetRandonSpawnPosition() + (Vector2)cam.transform.position);
                EnemySo enemyData = waveList.enemyType[enemyType];
                

                yield return new WaitForSeconds(waveList.enemySpawnTime);
            }
            yield return new WaitForSeconds(waveList.waveEndTime);
        }
    }

    public void UpgradeEnemy(EnemyBase enemyBase)
    {
        if (waveNum % 3 == 0)
        {

        }
        else
        {

        }
    }
}

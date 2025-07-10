using UnityEngine;

public class EnemySPown : MonoBehaviour
{
    private int enemypoolsize = 50;
    public GameObject enemyPrefab;
    private GameObject[] enemyPool;

    void Start()
    {
        enemyPool = new GameObject[enemypoolsize];
        for (int i = 0; i < enemypoolsize; i++)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyPool[i].SetActive(false);
        }
        enemyPool[0].SetActive(true);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawnerScript : MonoBehaviour
{
    public GameObject enemyType_1;
    public GameObject enemyType_2;
    public float spawnRadius = 2f;

    public int spawns = 5;
    public int enemiesInSpawnMax = 10;
    public int enemiesInSpawnMin = 5;
    public float startDelay = 3f;
    public float timeBetweenSpawnMax = 15f;
    public float timeBetweenSpawnMin = 7f;
    public float enemyType_1_Ratio = 0.8f;
    public float enemyType_2_Ratio = 0.2f;
 

    public int spawnsCount = 0;
    //private bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {
        //if (isActive)
        //{
        //    StartCoroutine("SpawnEnemy");
        //}     
    }


    public void InitDefault()
    {
        spawnsCount = 0;
        //isActive = false;
    }

    public void Init(SpawnerData spawnerData)
    {
        spawns = spawnerData.spawns;
        enemiesInSpawnMax = spawnerData.enemiesInSpawnMax;
        enemiesInSpawnMin = spawnerData.enemiesInSpawnMin;
        startDelay = spawnerData.startDelay;
        timeBetweenSpawnMax = spawnerData.timeBetweenSpawnMax;
        timeBetweenSpawnMin = spawnerData.timeBetweenSpawnMin;
        enemyType_1_Ratio = spawnerData.enemyType_1_Ratio;
        enemyType_2_Ratio = spawnerData.enemyType_2_Ratio;
        //isActive = true;
        spawnsCount = 0;
        StartCoroutine("SpawnEnemy");
    }



    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startDelay);
        while (spawnsCount <  spawns)
        {
            //Debug.Log(enemiesInSpawn * enemyType_1_Ratio);
            SpawnEnemyType(enemyType_1, enemyType_1_Ratio);
            //Debug.Log(enemiesInSpawn * enemyType_2_Ratio);
            SpawnEnemyType(enemyType_2, enemyType_2_Ratio);
            spawnsCount++;
            yield return new WaitForSeconds(Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax));
        }
        //for (int i = 0; i < spawns; i++)
        //{

        //}

    }

    private void SpawnEnemyType(GameObject enemyType, float enemyTypeRatio)
    {
        int enemiesInSpawn = Mathf.FloorToInt(Random.Range((float)enemiesInSpawnMin, enemiesInSpawnMax) * enemyTypeRatio);
        //Debug.Log(enemiesInSpawn);
        for (int i = 0; i < enemiesInSpawn; i++)
        {
            Instantiate(enemyType, (Vector2)transform.position + Random.insideUnitCircle * spawnRadius, Quaternion.Euler(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerDataList
{
    public List<SpawnerData> spawnerDatas = new List<SpawnerData>();

    public SpawnerDataList()
    {
        spawnerDatas = new List<SpawnerData>()
        {
            new SpawnerData()
        };
    }
}

[System.Serializable]
public class SpawnerData
{

    public int spawns = 5;
    public int enemiesInSpawnMax = 10;
    public int enemiesInSpawnMin = 5;
    public float startDelay = 3f;
    public float timeBetweenSpawnMax = 15f;
    public float timeBetweenSpawnMin = 7f;
    public float enemyType_1_Ratio = 0.8f;
    public float enemyType_2_Ratio = 0.2f;


    public SpawnerData()
    {
        spawns = 5;
        enemiesInSpawnMax = 10;
        enemiesInSpawnMin = 5;
        startDelay = 3f;
        timeBetweenSpawnMax = 15f;
        timeBetweenSpawnMin = 7f;
        enemyType_1_Ratio = 0.8f;
        enemyType_2_Ratio = 0.2f;
    }

}

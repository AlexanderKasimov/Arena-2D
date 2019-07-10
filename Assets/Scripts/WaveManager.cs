using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    public int waveNumber = 0;

    public List<SpawnerDataList> spawnerData;

    public List<GameObject> spawners;

    private List<GroupSpawnerScript> spawnersScripts;

    private int totalSpawnsInWave = 0;

    private int curSpawnsInWave = 0;

    public GameObject waveActivatorObject;

    public Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        //waveActivator = waveActivatorObject.GetComponent<WaveActivator>();
        spawnersScripts = new List<GroupSpawnerScript>();
        foreach (var item in spawners)
        {
            spawnersScripts.Add(item.GetComponent<GroupSpawnerScript>());
        }
        //StartWave();
    }

    public void StartWave()
    {
        totalSpawnsInWave = 0;
        curSpawnsInWave = 0;
        foreach (var item in  spawnersScripts)
        {
            item.InitDefault();
        }

        for (int i = 0; i < spawnerData[waveNumber].spawnerDatas.Count; i++)
        {
            spawnersScripts[i].Init(spawnerData[waveNumber].spawnerDatas[i]);
            totalSpawnsInWave += spawnerData[waveNumber].spawnerDatas[i].spawns;
        }
        waveNumber++;
        waveText.text = "Wave " + waveNumber;
        InvokeRepeating("CheckEndWave",1f,1f);               

    }

    public void CheckEndWave()
    {      
        curSpawnsInWave = 0;
        foreach (var item in spawnersScripts)
        {
            curSpawnsInWave += item.spawnsCount;
        }
        if (curSpawnsInWave == totalSpawnsInWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        CancelInvoke("CheckEndWave");
        waveActivatorObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        spawnerData = new List<SpawnerDataList>()
        {
            new SpawnerDataList()
        };

    }
}

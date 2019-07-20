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

    public GameObject waveInfoObject;

    public Image waveProgressBar;

    public Text waveText;

    public GameObject awardScriptObject;

    private AwardScript awardScript;

    // Start is called before the first frame update
    void Start()
    {
        //waveActivator = waveActivatorObject.GetComponent<WaveActivator>();
        spawnersScripts = new List<GroupSpawnerScript>();
        awardScript = awardScriptObject.GetComponent<AwardScript>();
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
        InvokeRepeating("CheckEndWave",0.1f,1f);
        awardScript.DestroyPickups();
        waveInfoObject.SetActive(true);
        UpdateUI();
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
        UpdateUI();
    }

    private void EndWave()
    {
        CancelInvoke("CheckEndWave");
    

        GameObject waveActivator = Instantiate(waveActivatorObject, transform);
        waveActivator.GetComponent<WaveActivator>().waveManagerObject = gameObject;
        
        //waveActivatorObject.SetActive(true);
        //waveActivatorObject.GetComponent<Animator>().enabled = false;
        awardScript.SpawnHealthPickup();
        if (waveNumber % 2 == 0)
        {
            awardScript.SpawnWeaponPickup(waveNumber);
        }

        GoblinDeath[] deathObjects = FindObjectsOfType<GoblinDeath>();
        foreach (var item in deathObjects)
        {
            item.StartDissolve();
        }

        waveInfoObject.SetActive(false);
        //switch (waveNumber)
        //{
        //   case 2:

        //        break;
        //   case 4:
        //        break;
        //    default:
        //        break;
        //}
    }

    private void UpdateUI()
    {
        //Debug.Log(curSpawnsInWave + " " + totalSpawnsInWave + " " + curSpawnsInWave / totalSpawnsInWave);
        waveProgressBar.fillAmount =1f * curSpawnsInWave / totalSpawnsInWave;
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

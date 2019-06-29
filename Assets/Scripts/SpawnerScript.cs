using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemyType;

    public float spawnRate = 2f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyType, transform.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

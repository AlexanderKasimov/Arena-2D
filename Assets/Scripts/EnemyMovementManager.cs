using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public float bigRadius;
    public float mediumRadius;
    public float smallRadius;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateRadius", 0f, 1f);
    }

    private void UpdateRadius()
    {
        float enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //bigRadius = 1f + 5 * enemiesCount / 100f;
        bigRadius = 6f;
        mediumRadius = 4f;
        smallRadius = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

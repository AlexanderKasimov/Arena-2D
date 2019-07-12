using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardScript : MonoBehaviour
{
    public GameObject healthPickupPlace;

    public GameObject healthPickupPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnHealthPickup()
    {
        Instantiate(healthPickupPrefab, healthPickupPlace.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }

    public void DestroyPickups()
    {
        HealthPickup[] healthPickups = FindObjectsOfType<HealthPickup>();
        foreach (var item in healthPickups)
        {
            Destroy(item.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

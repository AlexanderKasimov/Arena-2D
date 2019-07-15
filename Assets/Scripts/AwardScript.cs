using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardScript : MonoBehaviour
{
    public GameObject healthPickupPlace;

    public GameObject healthPickupPrefab;

    public GameObject weaponPickupPlace;

    public GameObject weaponPickupPrefab_1;

    public GameObject weaponPickupPrefab_2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnHealthPickup()
    {
        Instantiate(healthPickupPrefab, healthPickupPlace.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }


    public void SpawnWeaponPickup(int waveNumber)
    {
        switch (waveNumber)
        {
            case 2:
                Instantiate(weaponPickupPrefab_1, weaponPickupPlace.transform.position, Quaternion.Euler(0f, 0f, 0f));
                break;
            case 4:
                Instantiate(weaponPickupPrefab_2, weaponPickupPlace.transform.position, Quaternion.Euler(0f, 0f, 0f));
                break;
            default:
                break;
        }


    }

    public void DestroyPickups()
    {

        HealthPickup[] healthPickups = FindObjectsOfType<HealthPickup>();
        foreach (var item in healthPickups)
        {
            Destroy(item.gameObject);
        }

        WeaponPickupScript[] weaponPickups = FindObjectsOfType<WeaponPickupScript>();
        foreach (var item in weaponPickups)
        {
            Destroy(item.gameObject);
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{

    public float activationRadius = 1f;

    public GameObject weaponPrefab;

    //public GameObject waveManagerObject;

    private GameObject player;

    private PlayerScript playerScript;

    private bool isReady;

    public GameObject useButtonObject;

    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        Invoke("Ready", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = (player.transform.position - transform.position).magnitude;
        if (Input.GetButton("Use") && distanceToTarget < activationRadius && isReady && !playerScript.ws.isReloading )
        {    
            playerScript.PickupWeapon(weaponPrefab);     
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }

        if (distanceToTarget < activationRadius && isReady)
        {
            useButtonObject.SetActive(true);
        }
        else
        {
            useButtonObject.SetActive(false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }

    private void Ready()
    {
        isReady = true;
    }
}

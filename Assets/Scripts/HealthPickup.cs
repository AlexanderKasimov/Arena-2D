using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float activationRadius = 1f;

    public float HPregen = 1f;

    //public GameObject waveManagerObject;

    private GameObject player;

    private PlayerScript playerScript;

    private WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        //waveManager = waveManagerObject.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = (player.transform.position - transform.position).magnitude;
        if (Input.GetButton("Use") && distanceToTarget < activationRadius)
        {
            playerScript.RegenHealth(HPregen);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}

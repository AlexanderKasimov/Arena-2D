using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveActivator : MonoBehaviour
{
    public float activationRadius = 0.3f;

    public GameObject waveManagerObject;

    private GameObject player;

    private WaveManager waveManager;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waveManager = waveManagerObject.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = (player.transform.position - transform.position).magnitude;
        if (Input.GetButton("Use") && distanceToTarget < activationRadius)
        {
            waveManager.StartWave();
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}

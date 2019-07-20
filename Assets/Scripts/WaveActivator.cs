using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveActivator : MonoBehaviour
{
    public float activationRadius = 0.3f;

    public GameObject waveManagerObject;

    private GameObject player;

    private WaveManager waveManager;

    public GameObject useButtonObject;

    private Animator animator;

    private bool isReady;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waveManager = waveManagerObject.GetComponent<WaveManager>();
        animator = GetComponent<Animator>();
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = (player.transform.position - transform.position).magnitude;
        if (Input.GetButton("Use") && distanceToTarget < activationRadius && isReady)
        {
            if (waveManager.waveNumber == 5)
            {
                Debug.Log("End");
            }

            isReady = false;
            animator.enabled = true;   
            Invoke("StartWave", 2f);           
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

    private void StartWave()
    {
        waveManager.StartWave();
        Destroy(gameObject);   
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}

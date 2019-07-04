using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour
{
    public float timeBeforeDestroy = 1f;

    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, timeBeforeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

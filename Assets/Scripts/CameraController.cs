using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    public float lerpSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 objectPosition = target.transform.position;
        Vector2 direction = (mousePosition - objectPosition).normalized;

        Vector3 desiredPosition = (Vector3)(objectPosition + direction * (mousePosition - objectPosition).magnitude / 3f) + new Vector3(0, 0, -10);
        //Vector3 lerpPosition = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed * Time.deltaTime);           
        Vector3 lerpPosition_Mult = transform.position+(desiredPosition - transform.position)*(lerpSpeed * Time.deltaTime);
        //Debug.Log(lerpPosition+" "+lerpPosition_Mult);

        transform.position = lerpPosition_Mult;

    }
}

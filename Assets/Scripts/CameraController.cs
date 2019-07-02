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
        //Vector2 objectPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        Vector2 direction = (mousePosition - objectPosition).normalized;
        //Debug.DrawRay(target.transform.position, direction * (mousePosition - objectPosition).magnitude, Color.red);
        //Debug.Log((mousePosition - objectPosition).magnitude);
        //if ((mousePosition - objectPosition).magnitude > 5f)
        //{
        //    direction = direction * 0.5f;
        //}
        Vector3 desiredPosition = (Vector3)(objectPosition + direction * (mousePosition - objectPosition).magnitude / 2f) + new Vector3(0, 0, -10);
        Vector3 lerpPosition = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed * Time.deltaTime);

        transform.position = lerpPosition;

    }
}

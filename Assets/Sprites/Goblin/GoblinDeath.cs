using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDeath : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 startPoint;

    public Vector2 endPoint;

    public Vector2 lerpPoint;

    public Vector2 direction;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        startPoint = transform.position;
        endPoint = startPoint + direction * 1.5f;
    }

    // Update is called once per frame

    private float count = 0.0f;

    

    void Update()
    {
        if (direction.x < 0)
        {
            sr.flipX = false;
        }
        if (direction.x > 0)
        {
            sr.flipX = true;
        }


        if (count < 1.0f)
        {
            count += 3.0f * Time.deltaTime;

            //Vector3 m1 = Vector3.Lerp(startPoint, endPoint, count);
            //Vector3 m2 = Vector3.Lerp(lerpPoint, endPoint, count);
            transform.position = Vector3.Lerp(startPoint, endPoint, count);
        }
    }
}

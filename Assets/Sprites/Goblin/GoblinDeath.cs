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

    public Material blinkingMat;

    public float blinkDuration = 0.2f;

    private Material defaultMat;

    private float count = 0.0f;

    //private float time;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        startPoint = transform.position;
        endPoint = startPoint + direction * 1.5f;
        sr.material = blinkingMat;
        Invoke("SetDefaultMat",blinkDuration);
        Invoke("StartDissolve", 45f);      
       
    }

   void SetDefaultMat()
    {
        sr.material = defaultMat;
    }
    
    public void StartDissolve()
    {
        StartCoroutine("Dissolve");   
    }


    IEnumerator Dissolve()
    {
        CancelInvoke("StartDissolve");
        float time = 0f;
        while (time < 3)
        {
            time = time + Time.deltaTime;
            sr.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, time/0.8f));
            yield return null;
        }
        Destroy(gameObject);
    }

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

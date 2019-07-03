using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingScript : MonoBehaviour
{

    private SpriteRenderer sr;

    public bool isBlinking;

    private bool isBlink;

    public Material blinkMaterial;

    private Material startMaterial;

    public float blinkingDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startMaterial = sr.material;

        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (isBlinking)
            {
                yield return new WaitForSeconds(blinkingDuration);
                isBlink = !isBlink;
                if (isBlink)
                {
                    sr.material = blinkMaterial;

                }
                else
                {
                    sr.material = startMaterial;
                }
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

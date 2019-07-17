using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float speed = 10f;

    public Vector2 movementDir;

    public float damage = 1f;

    private bool collided; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collided = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Debug.Log(collision.gameObject);
    //    collision.gameObject.GetComponent<EnemyScript>().HandleDamage(damage);         
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Damage");       
        EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
        if (enemyScript != null)
        {
           if (!collided)
            {
                collided = true;
                //enemyScript.hitDirection = movementDir;
                enemyScript.HandleDamage(damage, movementDir);
                Destroy(gameObject);
            }

        }
        else
        {
            EnemyCasterScript enemyCasterScript = collision.gameObject.GetComponent<EnemyCasterScript>();
            if (enemyCasterScript != null)
            {
                if (!collided)
                {
                    collided = true;
                    enemyCasterScript.hitDirection = movementDir;
                    enemyCasterScript.HandleDamage(damage);
                    Destroy(gameObject);
                }
            }
        }   
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private SpriteRenderer sr;

    //private Animator animator;

    private GameObject target;

    public float speed = 2f;    

    private Vector2 movementDir;

    public float MaxHP = 3f;

    private float HP;

    public GameObject deathObject;




    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        HP = MaxHP;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        //Перенести в корутину        
        movementDir = (target.transform.position - transform.position).normalized;

        rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);

        //Sprite Rotation
        if (movementDir.x < 0)
        {
            sr.flipX = true;
        }
        if (movementDir.x > 0)
        {
            sr.flipX = false;
        }

    }

    public void HandleDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(deathObject, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}

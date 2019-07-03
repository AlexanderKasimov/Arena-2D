using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCasterScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private SpriteRenderer sr;

    //private Animator animator;

    private GameObject target;

    private Vector2 movementDir;

    public GameObject deathObject;

    public GameObject attackEffect;

    public GameObject fireballPrefab;

    public float MaxHP = 2f;
    private float HP;

    public float damage = 1f;

    public float speed = 2f;

    public float attackRange = 10f;
    public float delayBeforeAttack = 1f;
    public float delayAfterAttack = 0.3f;

    private bool isAttacking;

    public Vector2 hitDirection;

    //private Vector2 offset;


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
        //offset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        if (distanceToTarget < attackRange && !isAttacking)
        {
            StartCoroutine("Attack");
        }

    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(delayBeforeAttack);
        GameObject bullet = Instantiate(fireballPrefab, (Vector2)transform.position + movementDir * 0.3f, Quaternion.Euler(0, 0, 0));
        FireballScript fireballScript = bullet.GetComponent<FireballScript>();
        fireballScript.damage = damage;
        //Vector2 spreadDir = movementDir;
        //float angle = Random.Range(-maxInaccuracyAngle * (1 / accuracy), maxInaccuracyAngle * (1 / accuracy)) * Mathf.Deg2Rad;
        //spreadDir.x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
        //spreadDir.y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);

        fireballScript.movementDir = movementDir;
        yield return new WaitForSeconds(delayAfterAttack);
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        //Перенести в корутину        
        if (!isAttacking)
        {
            
            //movementDir = ((Vector2)target.transform.position+offset - (Vector2)transform.position).normalized;

            rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);
        }
        movementDir = (target.transform.position - transform.position).normalized;
        //Debug.DrawRay(transform.position, movementDir* ((Vector2)target.transform.position + offset - (Vector2)transform.position).magnitude);
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
        GoblinDeath goblinDeath = Instantiate(deathObject, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<GoblinDeath>();
        goblinDeath.direction = hitDirection;
        Destroy(gameObject);
    }

}

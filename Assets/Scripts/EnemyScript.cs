using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private SpriteRenderer sr;

    private Animator animator;

    private GameObject target;   

    private Vector2 movementDir;  

    public GameObject deathObject;

    public GameObject attackEffect;

    public float MaxHP = 3f;
    private float HP;

    public float damage = 1f;

    public float speed = 2f;

    public float attackRange = 0.5f;
    public float delayBeforeAttack = 0.3f;
    public float delayAfterAttack = 0.5f;

    private bool isAttacking;

    public Vector2 hitDirection;

    public Material blinkingMat;

    public float blinkDuration = 0.2f;
    
    private Material defaultMat;

    private bool isBlinking = false;

    public GameObject canvas;

    public Image hpBar;

    //private Vector2 offset;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        HP = MaxHP;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //offset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MovementDir", movementDir.magnitude);
        if (isAttacking)
        {
            animator.SetFloat("MovementDir", 0f);
        }
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
        GameObject attackObject = Instantiate(attackEffect, (Vector2)transform.position + movementDir * 0.6f,Quaternion.Euler(new Vector3(0,0, Vector2.SignedAngle(new Vector2(1, 0), movementDir))));
        //Debug.Log(Vector2.SignedAngle(new Vector2(1, 0), movementDir));
        RaycastHit2D hitResult = Physics2D.BoxCast((Vector2)transform.position+movementDir*0.3f, new Vector2(0.5f, 0.5f),0f,movementDir,0f,LayerMask.GetMask("Player"));
        if (hitResult.collider != null)
        {
            //Debug.Log(hitResult.collider.gameObject);
            PlayerScript player = hitResult.collider.gameObject.GetComponent<PlayerScript>();
            player.hitDirection = movementDir;
            player.HandleDamage(damage);           
            //hitResult.collider.gameObject.GetComponent<PlayerScript>().HandleDamage(damage);
           
        }
        yield return new WaitForSeconds(delayAfterAttack);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (isAttacking)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + movementDir * 0.3f, new Vector2(0.5f, 0.5f));
        }       

    }

    private void FixedUpdate()
    {
        //Перенести в корутину        
        if (!isAttacking)
        {
            movementDir = (target.transform.position - transform.position).normalized;
            //movementDir = ((Vector2)target.transform.position+offset - (Vector2)transform.position).normalized;

            rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);
        }

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
        if (!isBlinking)
        {
            StartCoroutine("Blink");
        }
        canvas.SetActive(true);
        hpBar.fillAmount = HP / MaxHP;

        if (HP <= 0)
        {
            Death();
        }
    }

    IEnumerator Blink()
    {
        isBlinking = true;
        sr.material = blinkingMat;
        yield return new WaitForSeconds(blinkDuration);
        sr.material = defaultMat;
        isBlinking = false;
    }

    private void Death()
    {
        GoblinDeath goblinDeath = Instantiate(deathObject, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<GoblinDeath>();
        goblinDeath.direction = hitDirection;
        Destroy(gameObject);
    }
}

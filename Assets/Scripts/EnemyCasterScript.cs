using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCasterScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private SpriteRenderer sr;

    private Animator animator;

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

    public Material blinkingMat;

    public float blinkDuration = 0.2f;

    private Material defaultMat;

    private bool isBlinking = false;

    public GameObject canvas;

    public Image hpBar;

    //private Vector2 offset;


    private Vector2 movementPoint;

    private Vector2 movementPointOffset;


    private Vector2 impactVector;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        // sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMat = sr.material;
        HP = MaxHP;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //offset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        canvas.SetActive(false);
        InvokeRepeating("GenerateMovementPointOffset", 0.1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {    
        animator.SetFloat("MovementDir", movementDir.magnitude);
        if (isAttacking)
        {
            animator.SetFloat("MovementDir", 0f);
        }
        if (target != null)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            if (distanceToTarget < attackRange && !isAttacking)
            {
                StartCoroutine("Attack");
            }
        }

    }

    private void GenerateMovementPointOffset()
    {
        movementPointOffset = Random.insideUnitCircle * attackRange;
    }

    IEnumerator Attack()
    {
        isAttacking = true;       
        yield return new WaitForSeconds(delayBeforeAttack);
        //GameObject attackObject = Instantiate(attackEffect, (Vector2)transform.position + movementDir * 0.6f, Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), movementDir))));
        Vector2 aimDir = (target.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(fireballPrefab, (Vector2)transform.position + aimDir * 0.3f, Quaternion.Euler(0, 0, 0));
        FireballScript fireballScript = bullet.GetComponent<FireballScript>();
        fireballScript.damage = damage;
        //Vector2 spreadDir = movementDir;
        //float angle = Random.Range(-maxInaccuracyAngle * (1 / accuracy), maxInaccuracyAngle * (1 / accuracy)) * Mathf.Deg2Rad;
        //spreadDir.x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
        //spreadDir.y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);

        fireballScript.movementDir = aimDir;
        yield return new WaitForSeconds(delayAfterAttack);
        isAttacking = false;
    }

    private void FixedUpdate()
    {
             
        if (!isAttacking)
        {      
            movementPoint = (Vector2)target.transform.position + movementPointOffset;
            movementDir = (movementPoint - (Vector2)transform.position).normalized;

            Vector2 resultVector = movementDir * speed + impactVector;            
            rb2d.MovePosition(rb2d.position + resultVector * Time.fixedDeltaTime);
        }
        else
        {
            Vector2 resultVector = impactVector;                       
            rb2d.MovePosition(rb2d.position + resultVector * Time.fixedDeltaTime);
        }


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

    public void HandleDamage(float damage, Vector2 hitDirection)
    {
        HP -= damage;
        this.hitDirection = hitDirection;
        if (HP <= 0)
        {
            Death();
            return;
        }
        //Knockback
        CancelInvoke("ResetImpactVector");
        impactVector = hitDirection * 6f;
        Invoke("ResetImpactVector", 0.1f);

        if (!isBlinking)
        {
            StartCoroutine("Blink");
        }
        canvas.SetActive(true);
        hpBar.fillAmount = HP / MaxHP;


    }

    private void ResetImpactVector()
    {
        impactVector = Vector2.zero;
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

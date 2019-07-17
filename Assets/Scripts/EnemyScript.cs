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

    //public GameObject waveManagerObject;

    //private WaveManager waveManager;

    public float MaxHP = 3f;
    private float HP;

    public float damage = 1f;

    public float speed = 2f;

    public float attackRange = 0.5f;
    public float delayBeforeAttack = 0.3f;
    public float delayAfterAttack = 0.5f;

    private bool isAttacking;

    private Vector2 impactVector;

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

    private float movementPointRadius;

    //private GameObject movementManagerObject;

    private EnemyMovementManager movementManager;

    private string curRadius; 


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        //sr = GetComponent<SpriteRenderer>();
        //waveManager = waveManagerObject.GetComponent<WaveManager>();
        //movementManager = movementManagerObject.GetComponent<EnemyMovementManager>();
        defaultMat = sr.material;
        HP = MaxHP;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        movementManager = FindObjectOfType<EnemyMovementManager>();
        //offset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        canvas.SetActive(false);
        movementPointRadius = movementManager.bigRadius;
        InvokeRepeating("GenerateMovementPointOffset", 0.1f, 5f);
        //GenerateMovementPoint();
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
        //switch (curRadius)
        //{
        //    case "big":
                

        //        break;
        //    case "medium": break;
        //    case "small": break;
        //    default:
        //        break;
        //}
        movementPointOffset = Random.insideUnitCircle * movementPointRadius; 
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
            PlayerScript player = hitResult.collider.gameObject.GetComponent<PlayerScript>();
            player.hitDirection = movementDir;
            player.HandleDamage(damage);        
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
        if (target != null)
        {
            Gizmos.DrawWireSphere(target.transform.position, movementPointRadius);      
            Gizmos.DrawWireCube(movementPoint, new Vector2(0.2f, 0.2f));
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.transform.position, movementManager.bigRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.transform.position, movementManager.mediumRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.transform.position, movementManager.smallRadius);
        }

    }

    private void FixedUpdate()
    {
        //Перенести в корутину        
        if (!isAttacking)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            if (distanceToTarget > movementManager.bigRadius )
            {
                curRadius = "big";
                movementPointRadius = movementManager.bigRadius;
                //movementPoint = (Vector2)target.transform.position + movementPointOffset;
            }
            else
            {
                if (distanceToTarget > movementManager.mediumRadius)
                {                
                    if (!string.Equals(curRadius, "medium"))
                    {
                        curRadius = "medium";
                        movementPointRadius = movementManager.mediumRadius;
                        GenerateMovementPointOffset();
                    }   
                }
                else
                {
                    if (distanceToTarget > movementManager.smallRadius)
                    {
                        if (!string.Equals(curRadius, "small"))
                        {
                            curRadius = "small";
                            movementPointRadius = movementManager.smallRadius;
                            GenerateMovementPointOffset();
                        }
                    }
                    else
                    {
                        if (!string.Equals(curRadius, "close"))
                        {
                            curRadius = "close";
                            movementPointRadius = 0f;
                            GenerateMovementPointOffset();
                        }               
                    }
                }
           
            }
            movementPoint = (Vector2)target.transform.position + movementPointOffset;        
            movementDir = (movementPoint - (Vector2)transform.position).normalized;
            Vector2 resultVector = movementDir * speed + impactVector;
            //impactVector = Vector2.zero;
            //movementDir = ((Vector2)target.transform.position+offset - (Vector2)transform.position).normalized;
            //rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);                   
            rb2d.MovePosition(rb2d.position + resultVector * Time.fixedDeltaTime);                   
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
        Invoke("ResetImpactVector",0.1f);

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

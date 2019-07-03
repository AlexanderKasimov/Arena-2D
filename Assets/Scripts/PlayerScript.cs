using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed = 2f;

    public float weaponAngle;

    private Animator animator;

    private SpriteRenderer sr;

    private Vector2 movementDir;

    public GameObject weapon;

    private WeaponScript ws;    
   
    private float timeSinceFire = 0.0f;


    public float MaxHP = 3f;

    private float HP;

    public GameObject deathObject;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ws = weapon.GetComponent<WeaponScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeSinceFire = ws.rateOfFire;
        HP = MaxHP;
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
        Invoke("Restart", 0.2f);
       
       // Instantiate(deathObject, transform.position, Quaternion.Euler(0, 0, 0));
       // Destroy(gameObject);
    }

    private void Restart()
    {
        SceneManager.LoadScene("Arena_01");
    }


    // Update is called once per frame
    void Update()
    {        
        //Get Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementDir = new Vector2(horizontalInput, verticalInput).normalized;

        animator.SetFloat("MovementDir", movementDir.magnitude);

        timeSinceFire = timeSinceFire + Time.deltaTime;

        if (Input.GetButton("Fire1") && timeSinceFire > 60f/ws.rateOfFire && !ws.isReloading)
        {       
            ws.Fire();
            //Debug.Log("Fired");          
            timeSinceFire = 0.0f;
        }
    }

    private void FixedUpdate()
    { 

        //Move player
        rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);

        Vector2 mousePosition = Input.mousePosition;

        Vector2 objectPosition = Camera.main.WorldToScreenPoint(weapon.transform.position);

        float deltaRot = Mathf.Atan2(mousePosition.y - objectPosition.y, mousePosition.x - objectPosition.x)*Mathf.Rad2Deg;

        weapon.transform.rotation = Quaternion.Euler(0f, 0f, deltaRot);

        weaponAngle = weapon.transform.rotation.eulerAngles.z;

        if (weapon.transform.rotation.eulerAngles.z > 90 && weapon.transform.rotation.eulerAngles.z < 270)
        {            
            //transform.localScale =new Vector3 ( -1,transform.localScale.y,1);
            sr.flipX = true;
            weapon.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            //transform.localScale = new Vector3( 1, transform.localScale.y, 1);
            sr.flipX = false;
            weapon.GetComponent<SpriteRenderer>().flipY = false;
        }

        ////Sprite Rotation
        //if (movementDir.x < 0)
        //{
        //    sr.flipX = true;
        //}
        //if (movementDir.x > 0)
        //{
        //    sr.flipX = false;
        //}
    }

}

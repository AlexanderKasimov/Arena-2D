using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed = 2f;

    //public float weaponAngle;

    private Animator animator;

    private SpriteRenderer sr;

    private Vector2 movementDir;

    public GameObject weapon;

    public GameObject weaponSocket;

    [HideInInspector]
    public WeaponScript ws;    
   
    private float timeSinceFire = 0.0f;


    public float MaxHP = 3f;

    private float HP;

    public GameObject deathObject;

    public Vector2 hitDirection;

    public Material blinkingMat;

    public float blinkDuration = 0.2f;

    private Material defaultMat;

    private bool isBlinking = false;

    public Text hpBarText;

    public Image hpBarImage;

    public Slider reloadSlider;

    public Text ammoText;

    public Image weaponImage;

    public GameObject weaponPickupPlace;

    private Vector2 knockbackVector;

    private bool isInvulnerable; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ws = weapon.GetComponent<WeaponScript>();
        defaultMat = sr.material;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(sr.color);
        //sr.color = new Color(1, 1, 1, 1);

        timeSinceFire = 60f / ws.rateOfFire;
        HP = MaxHP;
        UpdateUI();
        isInvulnerable = false;
        //WeaponInit;
        reloadSlider.gameObject.SetActive(false);
        ws.reloadSlider = reloadSlider;
        ws.weaponImage = weaponImage;
        ws.ammoText = ammoText;
        ws.UpdateUI();
        ws.weaponImage.sprite = ws.weaponImageSprite;
        //Added----------------------------------
        //hpBarImage.fillAmount = 1;
        //hpBarText.text = HP + "/" + MaxHP;
        //---------------------------------------
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
     
        if (Input.GetButton("Fire1") && /*ws != null &&*/  ws.isReady && timeSinceFire > 60f/ws.rateOfFire && !ws.isReloading)
        {       
            ws.Fire();
            //KnockBack - мб лучше в ws?
            CancelInvoke("ResetKnockbackVector");
            knockbackVector = -1f *ws.direction * ws.playerKnockback;
            Invoke("ResetKnockbackVector", 0.1f);

            timeSinceFire = 0.0f;
        }
    }

    private void ResetKnockbackVector()
    {
        knockbackVector = Vector2.zero;
    }


    private void FixedUpdate()
    {

        //Move player
        Vector2 resultVector = movementDir * speed + knockbackVector;

        //rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);
        rb2d.MovePosition(rb2d.position + resultVector * Time.fixedDeltaTime);

        Vector2 mousePosition = Input.mousePosition;

        Vector2 objectPosition = Camera.main.WorldToScreenPoint(weapon.transform.position);

        float deltaRot = Mathf.Atan2(mousePosition.y - objectPosition.y, mousePosition.x - objectPosition.x)*Mathf.Rad2Deg;

        weapon.transform.rotation = Quaternion.Euler(0f, 0f, deltaRot);

        //weaponAngle = weapon.transform.rotation.eulerAngles.z;

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

    public void PickupWeapon(GameObject weaponPrefab)
    {
        Instantiate(ws.pickupPrefab, weaponPickupPlace.transform.position, Quaternion.Euler(0f, 0f, 0f));
        GameObject newWeapon = Instantiate(weaponPrefab, weaponSocket.transform);     
        Destroy(weapon.gameObject);
        weapon = newWeapon;
        ws = weapon.GetComponent<WeaponScript>();
        timeSinceFire = 60f / ws.rateOfFire;
        ws.reloadSlider = reloadSlider;
        ws.weaponImage = weaponImage;
        ws.ammoText = ammoText;
        ws.UpdateUI();
        ws.weaponImage.sprite = ws.weaponImageSprite;
    }


    public void RegenHealth(float regenHP)
    {
        //Debug.Log(Mathf.Clamp(regenHP + HP, 0f, MaxHP));
        HP = Mathf.Clamp(regenHP + HP, 0f, MaxHP);
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpBarImage.fillAmount = HP / MaxHP;
        hpBarText.text = HP + "/" + MaxHP;
    }


    public void HandleDamage(float damage)
    {
        if (!isInvulnerable)
        {
            HP -= damage;
            UpdateUI();
            if (HP <= 0)
            {
                Death();
                return;
            }
            isInvulnerable = true;
            Invoke("EndInvulnerable", 1.2f);

            //Hit VFX



            //sr.color = new Color(1, 1, 1, 0);

            //Added----------------------------------

            //---------------------------------------
            if (!isBlinking)
            {
                StartCoroutine("Blink");
            }

        }

    }

    private void EndInvulnerable()
    {
        isInvulnerable = false;
        StopCoroutine("InvulnerableBlink");
        sr.color = new Color(1, 1, 1, 1);
    }


    IEnumerator Blink()
    {
        isBlinking = true;
        sr.material = blinkingMat;
        yield return new WaitForSeconds(blinkDuration);
        sr.material = defaultMat;
        isBlinking = false;
        StartCoroutine("InvulnerableBlink");
    }

    IEnumerator InvulnerableBlink()
    {
        while (true)
        {
            sr.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
     
    }


    private void Death()
    {
        Debug.Log("Death");
        Invoke("Restart", 2f);
        GoblinDeath goblinDeath = Instantiate(deathObject, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<GoblinDeath>();
        goblinDeath.direction = hitDirection;
        gameObject.SetActive(false); 

        // Destroy(gameObject);
    }

    private void Restart()
    {
        SceneManager.LoadScene("Arena_02");
    }

}

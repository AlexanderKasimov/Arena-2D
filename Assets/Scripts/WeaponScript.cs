using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public GameObject muzzle;

    public GameObject bulletPrefab;

    public Sprite weaponImageSprite;

    public GameObject pickupPrefab;

    public Vector2 direction;

    //Euler angle +-
    public float maxInaccuracyAngle = 10f;

    public bool isReloading;

    //статы
    //RPM
    public float rateOfFire = 240f;

    public float accuracy = 10f;

    public float damage = 1f;

    public int magazineSize = 10;
    //in sec
    public float reloadTime = 1f;

    public float bulletSpeed = 20f;

    public float playerKnockback = 3f;

    private int curAmmo;


    public Text ammoText;

    public Image weaponImage;

    


    public Slider reloadSlider;


    private AudioSource fireSound;

    public bool isShotgun = false;

    public bool isReady;

    public int bulletCountInShell = 5;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {               
        curAmmo = magazineSize;
        isReloading = false;

        if (ammoText != null)
        {
            UpdateUI();
        } 
        //magazineImage.fillAmount = 1;
        //magazineText.text = (curAmmo + "/" + magazineSize);        

        fireSound = GetComponent<AudioSource>();
        isReady = false;
        Invoke("SetReady", 0.1f);
    }

    private void SetReady()
    {
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 objectPosition = Camera.main.WorldToScreenPoint(muzzle.transform.position);

        direction = (mousePosition - objectPosition).normalized;

        Debug.DrawRay(muzzle.transform.position, direction*100,Color.red);

        if (Input.GetButton("Reload") && !isReloading)
        {
            StartCoroutine("Reload");
        }
    }

    public void Fire()
    {
        //safe check
        if (isReloading)
        {
            return;
        }
   
        curAmmo--;

        UpdateUI();
        //magazineImage.fillAmount = curAmmo / (magazineSize * 1.0f);
        //magazineText.text = (curAmmo + "/" + magazineSize);
        if (isShotgun)
        {
            for (int i = 0; i < bulletCountInShell; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, 0));
                BulletScript bs = bullet.GetComponent<BulletScript>();
                bs.damage = damage;
                bs.speed = bulletSpeed;
                Vector2 spreadDir = direction;
                float angle = Random.Range(-maxInaccuracyAngle * (1 / accuracy), maxInaccuracyAngle * (1 / accuracy)) * Mathf.Deg2Rad;
                spreadDir.x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
                spreadDir.y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);

                bs.movementDir = spreadDir;
            }
      
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, 0));
            BulletScript bs = bullet.GetComponent<BulletScript>();
            bs.damage = damage;
            bs.speed = bulletSpeed;
            Vector2 spreadDir = direction;
            float angle = Random.Range(-maxInaccuracyAngle * (1 / accuracy), maxInaccuracyAngle * (1 / accuracy)) * Mathf.Deg2Rad;
            spreadDir.x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
            spreadDir.y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);

            bs.movementDir = spreadDir;
        }
 
        //VFX
        if (animator != null)
        {
            animator.Play("Fire",-1,0f);
        }


        fireSound.Play();
        
 
        if (curAmmo <= 0)
        {
            StartCoroutine("Reload");
            return;
        }
    }



    IEnumerator Reload()
    {
        isReloading = true;

        reloadSlider.gameObject.SetActive(true);
        reloadSlider.animator.speed = 1 / reloadTime;
        reloadSlider.animator.Play("ReloadAnimation");

        yield return new WaitForSeconds(reloadTime);

        curAmmo = magazineSize;

        reloadSlider.gameObject.SetActive(false);


        UpdateUI();
        //magazineImage.fillAmount = 1;


        isReloading = false;
    }

    public void UpdateUI()
    {
        ammoText.text = curAmmo + "/" + magazineSize;
    }

}

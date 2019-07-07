using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public GameObject muzzle;

    public GameObject bulletPrefab;

    private Vector2 direction;

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

    private int curAmmo;

    //Added for Magazine------------------------------------
    public Image magazineImage;
    public Text magazineText;
    //------------------------------------------------------

    //Added for slider--------------------------------------
    public Slider slider;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        curAmmo = magazineSize;
        isReloading = false;

        //Added for Magazine------------------------------------
        magazineImage.fillAmount = 1;
        magazineText.text = (curAmmo + "/" + magazineSize);
        //------------------------------------------------------

        //Added for slider--------------------------------------
        slider.gameObject.SetActive(false);
        //------------------------------------------------------

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 objectPosition = Camera.main.WorldToScreenPoint(muzzle.transform.position);

        direction = (mousePosition - objectPosition).normalized;

        Debug.DrawRay(muzzle.transform.position, direction*100,Color.red);     

    }

    public void Fire()
    {
        //safe check
        if (isReloading)
        {
            return;
        }
        if (curAmmo <= 0)
        {            
            StartCoroutine("Reload");
            return;
        }
        curAmmo--;
        //Added for Magazine------------------------------------
        magazineImage.fillAmount = curAmmo / (magazineSize*1.0f);
        magazineText.text = (curAmmo + "/" + magazineSize);
        //------------------------------------------------------

        Debug.Log(curAmmo);
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, 0));
        BulletScript bs = bullet.GetComponent<BulletScript>();
        bs.damage = damage;
        Vector2 spreadDir = direction;
        float angle = Random.Range(-maxInaccuracyAngle*(1/accuracy), maxInaccuracyAngle*(1 / accuracy)) * Mathf.Deg2Rad;
        spreadDir.x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
        spreadDir.y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);

        bs.movementDir = spreadDir;
    }

    IEnumerator Reload()
    {
        isReloading = true;

        //Added for slider-------------------------------------------
        slider.gameObject.SetActive(true);

        /*
        for (int i = 0; i < 100; i++)
        {
            slider.value = i / 100f;
            Debug.Log(i/100);
            yield return new WaitForSeconds(reloadTime/100f);
        }
        */
        slider.animator.speed = 1 / reloadTime;
        slider.animator.Play("ReloadAnimation");
        //------------------------------------------------------
        yield return new WaitForSeconds(reloadTime);


        //Added for slider-------------------------------------------
        slider.gameObject.SetActive(false);
        //------------------------------------------------------


        curAmmo = magazineSize;
        //Added for Magazine------------------------------------
        magazineImage.fillAmount = 1;
        magazineText.text = (curAmmo + "/" + magazineSize);
        //------------------------------------------------------
        isReloading = false;
    }

}

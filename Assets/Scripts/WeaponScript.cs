using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject muzzle;

    public GameObject bulletPrefab;

    private Vector2 direction;

    public float maxInaccuracyAngle = 10f;

    //статы
    //RPM
    public float rateOfFire = 240f;

    public float accuracy = 10f;

    public float damage = 1f;

    public int magazineSize = 10;

    public int curAmmo;

    // Start is called before the first frame update
    void Start()
    {
        curAmmo = magazineSize;
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
        curAmmo--;
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

}

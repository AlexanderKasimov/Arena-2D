using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject muzzle;

    public GameObject bulletPrefab;

    Vector2 direction;    

    // Start is called before the first frame update
    void Start()
    {
        
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
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, 0));
        BulletScript bs = bullet.GetComponent<BulletScript>();
        bs.movementDir = direction;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float speed = 6f;

    public Vector2 movementDir;

    public float damage = 1f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movementDir * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
        //enemyScript.hitDirection = movementDir;
        player.HandleDamage(damage);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveX;
    public float moveY;
    public GameObject bullet;
    private Rigidbody2D shot;
    public float cooldown;
    public Vector2 shotVelocity;

    public Vector2 currVelocity = new Vector2(0,0);
    public int maxSpeed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (rb.velocity == new Vector2(0, 0) && cooldown <= 0)
        {
            Shoot();
        }
        
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        // Movement

        rb.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
        if (currVelocity != Vector2.zero && (currVelocity.x * rb.velocity.x) == 0)
        {
            if (true)
            {
                cooldown += 0.25f;
            }
        }
        currVelocity = rb.velocity;
    }

    void Shoot()
    {
        
        shot = Instantiate(bullet.GetComponent<Rigidbody2D>(), rb.position, rb.transform.rotation) as Rigidbody2D;
        shot.velocity = new Vector2(0,10);
        cooldown = 0.5f;
        shotVelocity = shot.velocity;
    }
  
}

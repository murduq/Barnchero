using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveX;
    public float moveY;
    public GameObject bullet;
    private Rigidbody2D shot;
    public float cooldown;
    public Vector2 shotVelocity;
    public float strafeProtection;
    public float iFrameTimer;

    public Vector2 currVelocity = new Vector2(0,0);
    public int maxSpeed = 5;
    public int health = 10;
    public int maxHealth = 10;
    public bool isHit = false;
    
    public HealthController healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHP(maxHealth);
    }

    void Update()
    {
        FindClosestEnemy();
        Move();
        if (rb.velocity == new Vector2(0, 0) && cooldown <= 0)
        {
            Shoot();
        }
        if (isHit){
            iFrameTimer -= Time.deltaTime;
        }
        if (iFrameTimer <= 0){
            isHit = false;
        }
        
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        // Movement

        rb.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        // if (currVelocity != Vector2.zero && (currVelocity.x * rb.velocity.x) == 0)
        // {
        //     if (strafeProtection < 0.25f)
        //     {
        //         strafeProtection += 0.05f;
        //     }
        // }
        // currVelocity = rb.velocity;
        // if (cooldown <= 0.05f) 
        // {
        //     cooldown += strafeProtection;
        // }
        // if (strafeProtection >= 0.04f)
        // {
        //    strafeProtection -= 0.04f;
        // } 
    }

    void Shoot()
    {
        
        shot = Instantiate(bullet.GetComponent<Rigidbody2D>(), rb.position + new Vector2(transform.up.x * 0.5f, transform.up.y * 0.5f), rb.transform.rotation) as Rigidbody2D;
        shot.velocity = transform.up * 10;
        cooldown = 0.4f;
        shotVelocity = shot.velocity;
    }
  
  void FindClosestEnemy()
  {
      float distanceToCloseEnemy = Mathf.Infinity;
      GameObject closestEnemy = null;
      GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

      foreach (GameObject currEnemy in allEnemies) {
          float distanceToEnemy = (currEnemy.transform.position - this.transform.position).sqrMagnitude;
          if (distanceToEnemy < distanceToCloseEnemy) {
              distanceToCloseEnemy = distanceToEnemy;
              closestEnemy = currEnemy;
          }
          transform.up = closestEnemy.transform.position - transform.position;
          
      }
      if (allEnemies.Length == 0){
          transform.up = new Vector2 (0,1);
          cooldown = 0.4f;
      }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
      if (collision.gameObject.tag == "Enemy" && !isHit){
          isHit = true;
          iFrameTimer = 1.0f;
          health -= 2;
          healthBar.SetHP(health);
      }
      if (health == 0){
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
  }

}

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

    public int damage = 2;

    public float maxCooldown = 0.4f;

    public float cooldown;

    public float strafeProtection;

    public float iFrameTimer;

    public float maxSpeed = 5f;

    public int health;

    public int maxHealth = 10;

    public bool isHit = false;

    public HealthController healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHP (maxHealth);
        health = maxHealth;
    }

    void Update()
    {
        FindClosestEnemy();
        Move();
        if (rb.velocity == new Vector2(0, 0) && cooldown <= 0)
        {
            Shoot();
        }
        if (isHit)
        {
            iFrameTimer -= Time.deltaTime;
        }
        if (iFrameTimer <= 0)
        {
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
    }

    void Shoot()
    {
        shot =
            Instantiate(bullet.GetComponent<Rigidbody2D>(),
            rb.position +
            new Vector2(transform.up.x * 0.75f, transform.up.y * 0.75f),
            rb.transform.rotation) as
            Rigidbody2D;
        shot.velocity = transform.up * 10;
        BulletController bull = shot.GetComponent<BulletController>();
        bull.setDamage (damage);
        cooldown = maxCooldown;
    }

    void FindClosestEnemy()
    {
        float distanceToCloseEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currEnemy in allEnemies)
        {
            float distanceToEnemy =
                (currEnemy.transform.position - this.transform.position)
                    .sqrMagnitude;
            if (distanceToEnemy < distanceToCloseEnemy)
            {
                distanceToCloseEnemy = distanceToEnemy;
                closestEnemy = currEnemy;
            }
            transform.up = closestEnemy.transform.position - transform.position;
        }
        if (allEnemies.Length == 0)
        {
            transform.up = new Vector2(0, 1);
            cooldown = maxCooldown;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isHit)
        {
            health -= 2;
            TakeDamage();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        switch (trigger.gameObject.tag)
        {
            case "Health":
                health += 2;
                if (maxHealth < health)
                {
                    maxHealth = health;
                    healthBar.SetMaxHP (maxHealth);
                }
                healthBar.SetHP (health);
                Destroy(trigger.gameObject);
                break;
            case "Damage":
                Destroy(trigger.gameObject);
                damage = (int)(damage * 1.5f);
                break;
            case "Speed":
                Destroy(trigger.gameObject);
                maxSpeed *= 1.5f;
                break;
            case "Burn":
                Destroy(trigger.gameObject);
                maxCooldown *= 0.8f;
                break;
        }
    }

    IEnumerator iFrameFlash()
    {
        bool flash = false;
        while (isHit)
        {
            this.GetComponent<Renderer>().enabled = flash;
            flash = !flash;
            yield return new WaitForSeconds(.15f);
        }
        this.GetComponent<Renderer>().enabled = true;
    }

    public void setHP(int newHp)
    {
        health = newHp;
        TakeDamage();
        Debug.Log("u got shot lol get rekt");
    }

    public int getHP()
    {
        return health;
    }

    void TakeDamage(){
        if(!isHit){
            isHit = true;
            iFrameTimer = 1.0f;
            healthBar.SetHP (health);
            Debug.Log("flashing u now 😳");
            StartCoroutine(iFrameFlash());
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}

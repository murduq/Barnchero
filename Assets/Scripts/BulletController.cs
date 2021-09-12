using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime = 2.0f;
    public int damage = 2;
    public bool burn = false;
    public Collider2D myCollider;
    public GameObject[] bullets;
    public GameObject[] enemies;
    public PlayerController player;
    public int ricochets = 0;
    public int maxRicochets = 0;
    public string enemyBulletName = "enemy_bullet(Clone)";
    public string playerBulletName = "player_bullet(Clone)";
    public GameObject closestEnemy = null;
    public GameObject secondClosest = null;
    public GameObject[] allEnemies;
    public Rigidbody2D rb;
    public int burnDamage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (this.gameObject.name == enemyBulletName)
        {
            ignore("Enemy");
            lifetime = 3.0f;
        }
        else
        {
            burnDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getBurn();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
        }
        Destroy(this.gameObject, lifetime);
        myCollider = GetComponent<Collider2D>();
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in bullets)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        ignore("Water");

    }

    void Update()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (this.gameObject.name == playerBulletName && rb.velocity.magnitude < 6.9f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player" && this.gameObject.name == enemyBulletName)
        {
            Destroy(this.gameObject);
            player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
        else if (collision.gameObject.tag == "Enemy" && this.gameObject.name == playerBulletName)
        {
            if (allEnemies.Length < 2)
            {
                Destroy(this.gameObject);
            }
            if (ricochets > 0)
            {
                GameObject target = collision.gameObject.GetComponent<EnemyController>().getClosestEnemy();
                if (target == null){
                    this.transform.up = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
                }
                else {
                    this.transform.up = target.transform.position - this.transform.position;
                    this.transform.position =
                        collision.gameObject.transform.position + this.transform.up * 0.5f;
                    rb.velocity = this.transform.up * 15f;
                    //this.transform.position = target.transform.position;
                    ricochets -= 1;
                }
                
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void setDamage(int newDamage)
    {
        this.damage = newDamage;
    }

    public int getDamage()
    {
        return this.damage;
    }

    void ignore(string toIgnore)
    {
        enemies = GameObject.FindGameObjectsWithTag(toIgnore);
        foreach (GameObject obj in enemies)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public void addRicochet()
    {
        maxRicochets += 1;
        ricochets = maxRicochets;
    }

    public void resetRicochet()
    {
        maxRicochets = 0;
        ricochets = maxRicochets;
    }

    public int getBurn()
    {
        return burnDamage;
    }

}

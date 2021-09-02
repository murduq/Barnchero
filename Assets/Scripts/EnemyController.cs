using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp;

    public float speed;

    public GameObject spawnedEnemy;

    public GameObject[] allEnemies;

    private bool spawning;

    public HealthController healthBar;

    public GameObject[] dropList;

    public string[] enemyTypes = {"ranged"};

    public string type;

    private Rigidbody2D shot;

    public int damage;

    public float cooldown;

    public GameObject bullet;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = Random.Range(1, 11);
        healthBar.SetMaxHP (hp);
        speed = Random.Range(0.5f, 1.5f);
        type = enemyTypes[Random.Range(0,enemyTypes.Length)];
        damage = 2;
        cooldown = speed;
    }

    void Update()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (allEnemies.Length == 0 && !spawning)
        {
            for (int i = 0; i <= Random.Range(1, 5); i++)
            {
                StartCoroutine(spawn());
            }
            spawning = true;
        }

        if (this.tag == "Enemy")
        {
            Move();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            BulletController hit =
                collision.gameObject.GetComponent<BulletController>();

            Destroy(collision.collider.gameObject);
            hp -= hit.getDamage();
            healthBar.SetHP (hp);
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            Drop(Random.Range(0f, 5.0f));
        }
    }

    void Move()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector2 target = players[0].transform.position;
        float moveSpeed = speed * Time.deltaTime;
        switch (type){
            case "melee":
                transform.position =
                    Vector2.MoveTowards(transform.position, target, moveSpeed);
                break;
            case "ranged":
                transform.up = target - new Vector2(transform.position.x, transform.position.y);
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                }
                else {
                    shoot(target);
                }
                
            break;
        }
        
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(spawnedEnemy,
        new Vector2(Random.Range(-3.7f, 3.7f), Random.Range(-4.3f, 4.3f)),
        Quaternion.identity);
        spawning = false;
    }

    void Drop(float dropNum)
    {
        Debug.Log (dropNum);
        switch (dropNum)
        {
            case float n when n <= 0.25f:
                Debug.Log("Dropping Apple");
                Instantiate(dropList[0],
                transform.position,
                Quaternion.identity);
                break;
            case float n when n <= 0.5f:
                Debug.Log("Dropping Melon");
                Instantiate(dropList[1],
                transform.position,
                Quaternion.identity);
                break;
            case float n when n <= 0.75f:
                Debug.Log("Dropping Carrot");
                Instantiate(dropList[2],
                transform.position,
                Quaternion.identity);
                break;
            case float n when n <= 1f:
                Debug.Log("Dropping Orange");
                Instantiate(dropList[3],
                transform.position,
                Quaternion.identity);
                break;
        }
    }

    void shoot(Vector2 target)
    {
        
        shot =
            Instantiate(bullet.GetComponent<Rigidbody2D>(),
            rb.position +
            new Vector2(transform.up.x * 0.75f, transform.up.y * 0.75f),
            rb.transform.rotation) as
            Rigidbody2D;
        shot.velocity = transform.up * 3;
        BulletController bull = shot.GetComponent<BulletController>();
        bull.setDamage (damage);
        cooldown = speed;
    }
}

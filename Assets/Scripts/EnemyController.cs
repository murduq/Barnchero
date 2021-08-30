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

    void Awake()
    {
        hp = Random.Range(1, 10);
        speed = Random.Range(0.5f, 1.5f);
        healthBar.SetMaxHP (hp);
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
        transform.position =
            Vector2.MoveTowards(transform.position, target, moveSpeed);
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
}

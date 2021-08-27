using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 8;
    public float speed = 1;
    public GameObject spawnedEnemy;
    public GameObject[] allEnemies;
    private bool spawning;
    
    void Update() 
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (allEnemies.Length == 0 && !spawning) {
            
            for(int i=0; i <= Random.Range(1,5); i++){
                StartCoroutine(spawn());
            }
            spawning = true;
        }

        if (this.tag == "Enemy"){
            Move();
        }

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet"){
            Destroy(collision.collider.gameObject);
            hp -= 2;
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector2 target = players[0].transform.position;
        float moveSpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed);
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(spawnedEnemy, new Vector2(Random.Range(-3.7f,3.7f),Random.Range(-4.3f,4.3f)), Quaternion.identity);
        spawning = false;
    }
}


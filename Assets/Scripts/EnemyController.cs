using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 8;
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

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(spawnedEnemy, new Vector2(Random.Range(-3.7f,3.7f),Random.Range(-4.3f,4.3f)), Quaternion.identity);
        spawning = false;
    }
}


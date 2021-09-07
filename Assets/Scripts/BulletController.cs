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

    void Awake()
    {
        if (this.gameObject.name == enemyBulletName){
            ignore("Enemy");
            lifetime = 3.0f;
        }
        Destroy(this.gameObject, lifetime);
        myCollider = GetComponent<Collider2D>();
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in bullets) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        if (this.gameObject.name == enemyBulletName){
            ignore("Enemy");
        }
        ignore("Water");
        
    }

    void Update()
    {
        float distanceToCloseEnemy = Mathf.Infinity;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies.Length > 2){
            closestEnemy = allEnemies[0];
            secondClosest = allEnemies[1];                
            foreach (GameObject currEnemy in allEnemies)
            {
                float distanceToEnemy =
                    (currEnemy.transform.position - this.transform.position)
                        .sqrMagnitude;
                if (distanceToEnemy < distanceToCloseEnemy)
                {
                    distanceToCloseEnemy = distanceToEnemy;
                    secondClosest = closestEnemy;
                    closestEnemy = currEnemy;
                }
            }
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Player" && this.gameObject.name == enemyBulletName){
            Destroy(this.gameObject);
            player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
        else if(collision.gameObject.tag == "Enemy" && this.gameObject.name == playerBulletName){
            if (allEnemies.Length < 2){
                    Destroy(this.gameObject);
                }
            if(ricochets > 0) {
                //this.transform.position = allEnemies[1].transform.position;
                this.transform.position = secondClosest.transform.position;
                ricochets -= 1;
            }
            else {
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

    void ignore(string toIgnore){        
        enemies = GameObject.FindGameObjectsWithTag(toIgnore);
        foreach (GameObject obj in enemies) {
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

}

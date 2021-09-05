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

    public string enemyBulletName = "enemy_bullet(Clone)";
    public string playerBulletName = "player_bullet(Clone)";

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Player" && this.gameObject.name == enemyBulletName){
            Destroy(this.gameObject);
            player = collision.gameObject.GetComponent<PlayerController>();
            player.setHP(player.getHP()-damage);
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

}

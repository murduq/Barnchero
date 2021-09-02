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

    void Awake()
    {
        Destroy(this.gameObject, lifetime);
        myCollider = GetComponent<Collider2D>();
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in bullets) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
        }
        else if(collision.gameObject.tag == "Player"){
            
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
}

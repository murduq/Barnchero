using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 10;
    
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
}

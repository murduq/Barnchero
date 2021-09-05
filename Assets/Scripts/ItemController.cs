using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float lifetime = 6.0f;
    public float timer;
    public GameObject[] players;
    public float speed = 10f;
    void Awake()
    {
        timer = lifetime;
        Destroy(this.gameObject, lifetime);
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <=2.5f){
            StartCoroutine(iFrameFlash());
        }
        if (Vector2.Distance(players[0].transform.position, transform.position) <= 1.75f){
            Magnet();
        }        
    }

    void Magnet()
    {
        Vector2 target = players[0].transform.position;
        float moveSpeed = speed * Time.deltaTime;
        transform.position =
            Vector2.MoveTowards(transform.position, target, moveSpeed);
    }

    IEnumerator iFrameFlash()
    {
        bool flash = false;
        while (true)
        {
            this.GetComponent<Renderer>().enabled = flash;
            flash = !flash;
            yield return new WaitForSeconds(.25f);
        }
    }
}

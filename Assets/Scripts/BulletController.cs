using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime = 2.0f;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this.gameObject, lifetime);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

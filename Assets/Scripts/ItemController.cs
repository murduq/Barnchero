using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float lifetime = 5.0f;
    public float timer;
    void Awake()
    {
        timer = lifetime;
        Destroy(this.gameObject, lifetime);
    }
}

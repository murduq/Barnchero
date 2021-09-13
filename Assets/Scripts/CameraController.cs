using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public Camera cam;
    public float pMaxSpeed;
    public float currSpeedDiff;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pMaxSpeed = player.maxSpeed;
        cam = GetComponent<Camera>();
        currSpeedDiff = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = player.maxSpeed/pMaxSpeed;
        if (temp > currSpeedDiff)
        {
            currSpeedDiff = temp;
            ZoomOut(currSpeedDiff);
        }
    }

    void ZoomOut(float amount)
    {
        float zoomLevel;
        zoomLevel = (amount-1)/3f;
        cam.orthographicSize *= (1+zoomLevel);
    }
}

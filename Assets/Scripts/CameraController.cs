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
        if (player.speedMultiplier > currSpeedDiff)
        {
            currSpeedDiff = player.speedMultiplier;
            ZoomOut(currSpeedDiff);
        }
    }

    void ZoomOut(float amount)
    {
        float zoomLevel;
        zoomLevel = (amount-1)/5f;
        cam.orthographicSize += zoomLevel;
    }
}

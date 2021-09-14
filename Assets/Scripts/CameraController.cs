using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public Camera cam;
    public float pMaxSpeed;
    public float currSpeedDiff;
    public float zoomDuration = 1.0f;

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
            StartCoroutine(ZoomOut(currSpeedDiff));
        }
    }

    IEnumerator ZoomOut(float amount)
    {
        float timeElapsed = 0;
        float zoomLevel = (amount - 1) / 5f;
        float ogSize = cam.orthographicSize;
        while (timeElapsed < zoomDuration)
        {
            //cam.orthographicSize += zoomLevel;
            cam.orthographicSize = Mathf.Lerp(ogSize, ogSize + zoomLevel, timeElapsed / zoomDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cam.orthographicSize = ogSize + zoomLevel;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class cameraManager : MonoBehaviour
{
    //Written by Ossi Uusitalo
    public GameObject mainCamera,zoomIn, zoomOut, player;
    public float bottom, top, zoomOutlevel, normalOffset; // This will prevent the camera from going over the top or bottom of the level which would break immersion. "Do not look behind the curtain!"
    //Alas, you need to manually move the camera in editor to see near the top to write down the highest allowed value for the zoomed in camera to go before it sees over the background, breaking immersion.
    public bool isZoomedIn = true, canMove;
    public Vector3 testingStuff;
    // Start is called before the first frame update
    private void Start()
    {
        //testingStuff = zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().GetComponent<track>;
        player = zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Follow.gameObject;
        bottom = mainCamera.gameObject.transform.position.x;
        Zoom();
        zoomOutlevel = zoomOut.gameObject.GetComponent<CinemachineVirtualCamera>().LookAt.gameObject.transform.position.y;
        Debug.Log("ZoomOut level: " + zoomOutlevel);
    }


    // Update is called once per frame
    void Update()
    {
        
        if (mainCamera.transform.position.y < bottom)
        {
            
            //ToDo: Stop the cam from going lower.
        }
        if(mainCamera.transform.position.y > top)
        {
            // Will stop the camera from going higher
        }

        if (isZoomedIn)
        {
            zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 15;
        }
        else
        {
            mainCamera.transform.position = new Vector2(mainCamera.transform.position.x, zoomOutlevel);
            zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 5;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Zoom();
        }
    }

    public void Zoom()
    {
<<<<<<< Updated upstream
        float priority = zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Priority;
        // if( < zoomOut.gameObject.GetComponent<CinemachineVirtualCamera>().Priority)
=======
       if(isZoomedIn)
>>>>>>> Stashed changes
        {
            isZoomedIn = false;
        } else
        {
            isZoomedIn = true;
        }

    }
}

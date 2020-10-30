using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraManager : MonoBehaviour
{
    public GameObject zoomIn, zoomOut;
    public bool isZoomed = true;
    // Start is called before the first frame update

    void Update()
    {
        if(isZoomed)
        {
            zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 15;
        } else
        {
            zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 5;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Zoom();
        }
    }

    public void Zoom()
    {
        if(isZoomed)
        {
            isZoomed = false;
        } else
        {
            isZoomed = true;
        }

    }
}

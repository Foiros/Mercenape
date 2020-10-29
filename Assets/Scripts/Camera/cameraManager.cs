using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraManager : MonoBehaviour
{
    public GameObject zoomIn, zoomOut;
    // Start is called before the first frame update
    private void Start()
    {
        Zoom();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Zoom();
        }
    }

    public void Zoom()
    {
        float priority = zoomIn.gameObject.GetComponent<CinemachineVirtualCamera>().Priority;
        if( < zoomOut.gameObject.GetComponent<CinemachineVirtualCamera>().Priority)
        {

        }

    }
}

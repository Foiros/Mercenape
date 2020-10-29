using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(zoomIn.activeSelf == true)
        {
            zoomOut.SetActive(true);
            zoomIn.SetActive(false);
        } else
        {
            zoomOut.SetActive(false);
            zoomIn.SetActive(true);
        }

    }
}

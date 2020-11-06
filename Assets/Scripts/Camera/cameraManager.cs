using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraManager : MonoBehaviour
{
    //Made by Ossi Uusitalo
    CinemachineVirtualCamera cineVirtual; // Main CineMachine component
    CinemachineFramingTransposer cineComp; // This where we get the tracker's offset
    public float top,maxSize; // These will mark the max values for the ortographic size (how large the camera view is) and maximum offset for the tracker. The camera will be on its maximum values in editor before shifting to zooming in.
    public float bottom, minSize; //You need to manually set the CineMancer camera to the desired zoomed in camera and type down the Size and offset in the editor.
    public float zoomMultiplier; //This will dictate how much zoom is applied on the mouse wheel scroll. Each tick of a mouse wheel is 1, which is then multiplied by this float.
    public GameObject mainCamera;
    
    // Start is called before the first frame update

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
       cineVirtual = mainCamera.GetComponent<CinemachineVirtualCamera>();
            if(cineVirtual != null)
            {
                Debug.Log("CineVirtual found");
                cineComp = cineVirtual.GetCinemachineComponent<CinemachineFramingTransposer>();
                if (cineComp != null)
                {
                    top = cineComp.m_ScreenY;
                    maxSize = cineVirtual.m_Lens.OrthographicSize;
                    cineComp.m_ScreenY = bottom;
                    cineVirtual.m_Lens.OrthographicSize = minSize;
                Debug.Log("CineComp found");
                }
            }
    }

    void Update()
    {
        cineComp.m_ScreenY -= Input.mouseScrollDelta.y / 100 * zoomMultiplier;
        cineVirtual.m_Lens.OrthographicSize -= Input.mouseScrollDelta.y * zoomMultiplier;
        if(cineVirtual.m_Lens.OrthographicSize < minSize)
        {
            cineVirtual.m_Lens.OrthographicSize = minSize;
        }

        if(cineVirtual.m_Lens.OrthographicSize > maxSize)
        {
            cineVirtual.m_Lens.OrthographicSize = maxSize;
        }


        if(cineComp.m_ScreenY > top)
        {
            cineComp.m_ScreenY = top;
        }
        if(cineComp.m_ScreenY < bottom)
        {
            cineComp.m_ScreenY = bottom;
        }

    }

}

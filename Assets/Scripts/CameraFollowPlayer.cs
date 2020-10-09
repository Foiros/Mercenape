using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public bool isZoomedIn;
    public float zoomIn, zoomOut, zoomNormal;
    Camera cam;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        isZoomedIn = false;
        cam = transform.GetComponent<Camera>();
        zoomNormal = cam.orthographicSize;
        zoomIn = zoomNormal - 1;
        zoomOut = zoomNormal + 2;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 tempPos = transform.position;
        if (isZoomedIn)
        {
            tempPos.x = playerTransform.position.x + 10;
            tempPos.y = playerTransform.position.y + 5;
        } else
        {
            tempPos.x = playerTransform.position.x;
            tempPos.y = 1.5f;
            
        }
        transform.position = tempPos;
    }

    private void Update()
    {
        if(cam.orthographicSize <= zoomNormal)
            {
                isZoomedIn = true;
            } else if(cam.orthographicSize > zoomNormal)
            {
                isZoomedIn = false;
            }
        if(cam.orthographicSize >= zoomIn && cam.orthographicSize <= zoomOut && Time.timeScale == 1)
        {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * - 1;
                       
            if(cam.orthographicSize < zoomIn)
            {
                cam.orthographicSize = zoomIn;
            }
            if(cam.orthographicSize > zoomOut)
            {
                cam.orthographicSize = zoomOut;
            }
            
        }
        

    }
}

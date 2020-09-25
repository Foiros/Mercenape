using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 tempPos = transform.position;

        tempPos.x = playerTransform.position.x;
        tempPos.y = playerTransform.position.y;
        transform.position = tempPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Enemy move left direction
        transform.Translate(Time.deltaTime * speed * -1, 0, 0);
    }
}

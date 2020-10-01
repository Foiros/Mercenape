using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallToGround : MonoBehaviour
{
    [SerializeField] LayerMask groundlayermask;
   
    void Update()
    {
        bool grounded = Physics2D.OverlapCircle(transform.position, (0.3f), groundlayermask);

        if (!grounded)
        {
            transform.Translate(Vector2.down * 3 * Time.deltaTime);
        }
        
    }
}

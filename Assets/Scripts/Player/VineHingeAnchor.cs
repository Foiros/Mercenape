using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineHingeAnchor : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    bool isSwing=false;
    public float swingSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");


        if (isSwing)
        {
            player.transform.parent = this.transform;
           
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;// no more gravity
            
            rb.AddForce(new Vector2(h * swingSpeed * Time.deltaTime,0), ForceMode2D.Force);
            
          if (Input.GetKeyDown(KeyCode.Space)&& isSwing)
            {
                isSwing = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.transform.parent = null;

                 player.GetComponent<Rigidbody2D>().AddForce (new Vector2(player.GetComponent<Rigidbody2D>().velocity.x+ (h*swingSpeed*10*Time.deltaTime), player.GetComponent<Rigidbody2D>().velocity.y+20)*100 * Time.deltaTimea);
                
                

            }

        }
        else
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
        
        
    }
    
    
  
    private void OnTriggerStay2D  (Collider2D collision)
    {
        if (collision.gameObject.name == "Player" )
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 4f));

                isSwing = true;
            }
        }
    }

}



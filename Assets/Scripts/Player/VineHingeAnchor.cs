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
        float v = Input.GetAxisRaw("Vertical");

        if (isSwing)
        {
            player.transform.parent = this.transform;
           
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;// no more gravity
            
            rb.AddForce(new Vector2(h * swingSpeed * Time.deltaTime,0), ForceMode2D.Force);

            if (v != 0) {

                player.transform.Translate(Vector2.up*v*10*Time.deltaTime);
            }


                if (Input.GetKeyDown(KeyCode.Space) && isSwing)
            {
                isSwing = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.transform.parent = null;
                // need to come up with a formula for swing
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1)*swingSpeed*1000*Time.deltaTime);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                

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



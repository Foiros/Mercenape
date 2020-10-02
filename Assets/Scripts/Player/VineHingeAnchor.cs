using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VineHingeAnchor : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    public bool isSwing=false;
    public float swingSpeed;
    public UnityEvent VineEvent;
    float h, v;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        if(VineEvent == null)
        {
            VineEvent = new UnityEvent();
        }
        //VineEvent.AddListener(MovePlayer);

    }

    // Update is called once per frame
    void Update()
    {
         h = Input.GetAxis("Horizontal");
         v = Input.GetAxisRaw("Vertical");

        if (isSwing)
        {
            player.transform.parent = this.transform;
            player.transform.rotation = this.transform.rotation;

            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;// no more gravity

            rb.AddForce(new Vector2(h * swingSpeed * Time.deltaTime, 0), ForceMode2D.Force);

            if (v != 0)
            {

                player.transform.Translate(Vector2.up * v * 10 * Time.deltaTime);
            }


            

        }
        else
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
        
        
        if (Input.GetKeyDown(KeyCode.Space) && isSwing)
        {

            VineEvent.Invoke();
        }


    }


     private void FixedUpdate()
    {
        

       
    }

    

    void MovePlayer()
    {
        isSwing = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.transform.parent = null;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().MovePosition((Vector2)player.transform.position +Vector2.up*20*Time.deltaTime + Vector2.right*10000) ;
        print("event");


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



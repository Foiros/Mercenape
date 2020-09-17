using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float PlayerSpeed; // for move left and right
    public float PlayerJumpPow, PlayerDoubleJumpPow; // for jump
    public float MidAirSpeed; // for move left and right while mid air
    [SerializeField] private LayerMask groundlayermask;

    private Animator PlayerAnimator;
    private Rigidbody2D PlayerRigid2d;
    private BoxCollider2D PlayerboxCollider2d;
    private bool FaceRight = true;
    private bool PlayerDoubleJump;
  



    void Start()
    {
        PlayerRigid2d = transform.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
        PlayerboxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
        FlipPlayer();
        SetPlayerAnimator();// should have a script for animtor 
        PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

   

    private void PlayerMove()
    {

       

        if (PlayerGrounded()) // if player is move on the ground with normal speed
        {
            if (Input.GetKey(KeyCode.D))
            {
                PlayerRigid2d.velocity = new Vector2(PlayerSpeed, PlayerRigid2d.velocity.y); 

            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerRigid2d.velocity = new Vector2(-PlayerSpeed, PlayerRigid2d.velocity.y);

                }
                else
                {//No key is pressed
                    PlayerRigid2d.velocity = new Vector2(0, PlayerRigid2d.velocity.y);
                    PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezePositionX;

                }
            }

        }
        else // if player is jumping we can have them some control
        {
            if (Input.GetKey(KeyCode.D))
            {
                PlayerRigid2d.velocity += new Vector2(PlayerSpeed * MidAirSpeed*Time.deltaTime, 0);
                PlayerRigid2d.velocity = new Vector2(Mathf.Clamp(PlayerRigid2d.velocity.x, -PlayerSpeed, PlayerSpeed), PlayerRigid2d.velocity.y);

            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerRigid2d.velocity += new Vector2(-PlayerSpeed * MidAirSpeed * Time.deltaTime, 0);
                    PlayerRigid2d.velocity = new Vector2(Mathf.Clamp(PlayerRigid2d.velocity.x, -PlayerSpeed, PlayerSpeed), PlayerRigid2d.velocity.y);

                }
                else
                {//No key is pressed
                    PlayerRigid2d.velocity = new Vector2(0, PlayerRigid2d.velocity.y);
                    PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezePositionX;

                }
            }
        }

    }
    private void PlayerJump() // both single and double 
        // side note could handle jump power by * with the character height. at the moment the vector in middle of the character so 7pixel long
    {
        if (PlayerGrounded())
        {
            PlayerDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PlayerGrounded()) 
            {
                PlayerRigid2d.velocity = Vector2.up * PlayerJumpPow;
            }
            else
                if (PlayerDoubleJump)
            {
                PlayerRigid2d.velocity = Vector2.up * PlayerDoubleJumpPow;
                PlayerDoubleJump = false;
            }
        }
     }

    
    public bool PlayerGrounded() // check if player is on the ground
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(PlayerboxCollider2d.bounds.center, PlayerboxCollider2d.bounds.size, 0f, Vector2.down, .1f,groundlayermask);
       /* Debug.Log(raycastHit2D.collider);*/
        return raycastHit2D.collider != null;
    }

    private void SetPlayerAnimator()
    {
        bool IsGrounded = PlayerGrounded();
        PlayerAnimator.SetBool("PlayerGrounded", IsGrounded);
        PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(PlayerRigid2d.velocity.x));
        
    }

    private void FlipPlayer()
    {
        if (PlayerRigid2d.velocity.x < 0 && FaceRight == true) { Flip(); }
        if (PlayerRigid2d.velocity.x > 0 && FaceRight == false) { Flip(); }
    }

    private void Flip()
    {
        FaceRight = !FaceRight;
        Vector2 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

    }

 

}


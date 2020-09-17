using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float PlayerSpeed; // for move left and right
    public float PlayerJumpPow, PlayerDoubleJumpPow; // for jump
    public float MidAirSpeed; // for move left and right while mid air
    [SerializeField] private LayerMask groundlayermask;
    [SerializeField] private LayerMask walllayermask;


    private Animator PlayerAnimator;
    private Rigidbody2D PlayerRigid2d;
    private BoxCollider2D PlayerboxCollider2d;
    
    
    
    bool IsGrounded;
    public Transform PlayerUnderPos;
    private bool FaceRight = true;
    private bool PlayerDoubleJump;

    public Transform PlayerFrontPos, PlayerBehindPos;
    public float CheckRadius;
    

    bool IsTouchingFront;
    bool IsTouchingBehind;
    bool IsWallGrab = false;
    public float PlayerClimbSpeed;


    void Start()
    {
        PlayerRigid2d = transform.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
        PlayerboxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(PlayerUnderPos.position, CheckRadius, groundlayermask);
        Debug.Log(IsGrounded);
        IsTouchingFront = Physics2D.OverlapCircle(PlayerFrontPos.position, CheckRadius, walllayermask);
        IsTouchingBehind = Physics2D.OverlapCircle(PlayerBehindPos.position, CheckRadius, walllayermask);
        if (IsTouchingFront == true && Input.GetKey(KeyCode.Mouse1)|| IsTouchingBehind == true && Input.GetKey(KeyCode.Mouse1))
        {IsWallGrab = true;}
        else { IsWallGrab = false; }

        PlayerMove();
        PlayerJump();
        FlipPlayer();
        SetPlayerAnimator();// should have a script for animtor 
        PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerClimb();


    }
   

    void PlayerClimb()
    {
       
        if(IsWallGrab==true)
        {
            PlayerRigid2d.gravityScale = 0;
            PlayerRigid2d.velocity = new Vector2(0, 0);
           
        }
        
        if (!IsWallGrab)
        {
            PlayerRigid2d.gravityScale = 1;
            
        }

        if(IsWallGrab && Input.GetKey(KeyCode.W))
        {
            PlayerRigid2d.velocity += new Vector2(0, PlayerClimbSpeed);

        }
        if (IsWallGrab && Input.GetKey(KeyCode.S))
        {
            PlayerRigid2d.velocity += new Vector2(0, -PlayerDoubleJumpPow);
        }

    }

    private void PlayerMove()
    {

        

            if (IsGrounded) // if player is move on the ground with normal speed
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
                    PlayerRigid2d.velocity += new Vector2(PlayerSpeed * MidAirSpeed * Time.deltaTime, 0);
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
        if (IsGrounded)
        {
            PlayerDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsGrounded) 
            {
                PlayerRigid2d.velocity = Vector2.up * PlayerJumpPow;

            }
            else

            if (PlayerDoubleJump==true)
            {
                PlayerRigid2d.velocity = Vector2.up * PlayerDoubleJumpPow;
                PlayerDoubleJump = false;
               
            }
        }
     }

    
    

    private void SetPlayerAnimator()
    {
        
        PlayerAnimator.SetBool("PlayerGrounded", IsGrounded);
        PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(PlayerRigid2d.velocity.x));
        
    }

    private void FlipPlayer()
    {
        if (Input.GetKey(KeyCode.A) &&  FaceRight == true) { Flip(); }
        else if (Input.GetKey(KeyCode.D) && FaceRight == false) { Flip(); }
        
    }

    private void Flip()
    {
        FaceRight = !FaceRight;
        Vector3 Scale= transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerFrontPos.position, CheckRadius);

      

    }
}


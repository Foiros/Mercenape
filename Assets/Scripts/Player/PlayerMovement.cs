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
    
    
    
    bool IsGrounded;
    public Transform PlayerUnderPos;
    public bool FaceRight = true;
    private bool PlayerDoubleJump;

    public Transform PlayerFrontPos, PlayerBehindPos;
    public float CheckRadius;
    

    bool IsTouchingFront;
    bool IsTouchingBehind;
    bool IsWallGrab = false;
    public float PlayerClimbSpeed;

    public bool isPlayerBlock = false;

    void Start()
    {
        PlayerRigid2d = transform.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
       


    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(PlayerUnderPos.position, CheckRadius, groundlayermask);
        
        // to check if player collide with the climable surface both front and behind 
        IsTouchingFront = Physics2D.OverlapCircle(PlayerFrontPos.position, CheckRadius, walllayermask);
        IsTouchingBehind = Physics2D.OverlapCircle(PlayerBehindPos.position, CheckRadius, walllayermask);
        if ((IsTouchingFront == true )|| IsTouchingBehind == true)
        {IsWallGrab = true;}
        else { IsWallGrab = false; }

        // check if player click right mouse 
        if (Input.GetMouseButton(1))
        {
            isPlayerBlock = true;
        }
        else
        {
            isPlayerBlock = false;
        }

        SetPlayerAnimator();// should have a script for animtor 


        PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        PlayerMove();
        PlayerJump();
        FlipPlayer();
        PlayerClimb();

    }

    void FixedUpdate()
    {

        

    }

    void PlayerClimb()
    {
       
        if(IsWallGrab==true)
        {
            if (Input.GetKey(KeyCode.E)) {
                PlayerRigid2d.gravityScale = 0;
                PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.up * PlayerClimbSpeed * Time.deltaTime);
                
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.left * PlayerClimbSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.right * PlayerClimbSpeed * Time.deltaTime);
                }
            }
            

           else if (!Input.GetKey(KeyCode.E))
            {
                PlayerRigid2d.gravityScale = 3;
                
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerRigid2d.gravityScale = 3;
                    PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.left * PlayerSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    PlayerRigid2d.gravityScale = 3;
                    PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.right * PlayerSpeed * Time.deltaTime);
                }
            }

        }
        else { PlayerRigid2d.gravityScale = 3; }
        
        

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
        if (IsGrounded|| IsWallGrab)
        {
            PlayerDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
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

    // TODO make a end and start time to check if moving button held more than some sec to increase or decrease move speed
    /*void CheckMoveButtonTime()
    {
        float endTime, startTime;

        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {


        }
    }*/
    

    private void SetPlayerAnimator()
    {
        
        PlayerAnimator.SetBool("PlayerGrounded", IsGrounded);
        PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(PlayerRigid2d.velocity.x));
        PlayerAnimator.SetBool("IsPlayerBlock", isPlayerBlock);
        
    }

    private void FlipPlayer()
    {
        if (Input.GetKey(KeyCode.A)  &&  FaceRight == true) { Flip(); }
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


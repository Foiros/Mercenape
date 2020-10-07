using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public float PlayerSpeed; // for move left and right
    public float PlayerJumpPow, PlayerDoubleJumpPow; // for jump
    public float MidAirSpeed; // for move left and right while mid air
    [SerializeField] private LayerMask groundlayermask, walllayermask, ladderlayermask;
    
    

    private Animator PlayerAnimator;
    private Rigidbody2D PlayerRigid2d;

    float inputH; 
    
    bool IsGrounded;
    
    public bool FaceRight = true;
    public bool PlayerDoubleJump;

    public Transform PlayerFrontPos, PlayerBehindPos;
    public Transform PlayerUnderPos, PlayerAbovePos;
    public float CheckRadius;
    

    bool IsTouchingFront;
    bool IsTouchingBehind;
    
    
    bool isOnTop;
    bool IsWallGrab = false;
    public float PlayerClimbSpeed;

    public bool isPlayerBlock = false;

   
    void Start()
    {
        PlayerRigid2d = transform.GetComponent<Rigidbody2D>();
        PlayerAnimator = transform.GetComponent<Animator>();
      

    }

    void Update()
    {
        CheckPlayerGrounded();
       //CheckPlayerGrabWall();
        CheckPlayerBlock();
        CheckClimbLadder();


       InputHorrizontal();
        FlipPlayer();

        PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (!isPlayerBlock) { 
            PlayerJump();
            

        }


        SetPlayerAnimator();// could change to call animation if needed

    }

    void FixedUpdate()
    {

        if (!isPlayerBlock)// when player is not blocking they can move
        {
            PlayerMove();
            PlayerClimbLadder();
            PlayerClimbWal();
            PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerRigid2d.velocity = new Vector2(0, 0);
           PlayerRigid2d.MovePosition((Vector2)transform.position +Vector2.right * 100 * Time.deltaTime + Vector2.up * 100 * Time.deltaTime);
        }
    }





    void CheckPlayerGrounded()
    {
        IsGrounded = Physics2D.OverlapCircle(PlayerUnderPos.position, CheckRadius, groundlayermask);
    }




    void CheckPlayerBlock()
    {
        // check if player click right mouse 
        if (Input.GetMouseButton(1))
        {
            isPlayerBlock = true;
        }
        else
        {
            isPlayerBlock = false;
        }
    }

    void InputHorrizontal()
    {
        inputH = Input.GetAxisRaw("Horizontal");// Note if dont get raw axis it feels like splippery
    }
   
    
    // check collide with wall 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall")&& IsWallGrab==false)
        {
            IsWallGrab = true;
           
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("wall") && IsWallGrab == true)
        {
            IsWallGrab = false;
            
        }
    }
    
    //player climb on wall
    void PlayerClimbWal()
    {
        isOnTop = (Physics2D.OverlapCircle(PlayerAbovePos.position, CheckRadius, walllayermask) == false && Physics2D.OverlapCircle(PlayerUnderPos.position, CheckRadius, walllayermask) == true);

        if (IsWallGrab == true)
        {
            if (!isOnTop)//if not on top
            {
                if (Input.GetKey(KeyCode.E))// climb up
                {
                    PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.up * PlayerClimbSpeed * Time.deltaTime );
                    if (inputH != 0)
                    {
                        PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.right * inputH * Time.deltaTime);

                    }
                }
            }
        }
      
    }

    
    /*  void CheckPlayerGrabWall()
  {
      // to check if player collide with the climable surface both front and behind 
      IsTouchingFront = Physics2D.OverlapCircle(PlayerFrontPos.position, CheckRadius, walllayermask);
      IsTouchingBehind = Physics2D.OverlapCircle(PlayerBehindPos.position, CheckRadius, walllayermask);

      if ((IsTouchingFront == true) || IsTouchingBehind == true)
      { IsWallGrab = true; }
      else { IsWallGrab = false; }
  }
*/

    //bool for player 2 climb on ladder
    bool CheckClimbLadder()
    {
        return (Physics2D.OverlapCircle(PlayerAbovePos.position, CheckRadius, ladderlayermask)); 
        
        
    }
    bool CheckOnTopLadder()
    {
        return (!Physics2D.OverlapCircle(PlayerAbovePos.position, CheckRadius, ladderlayermask) && (Physics2D.OverlapCircle(PlayerUnderPos.position, CheckRadius, ladderlayermask)));
           

    }



    // player climb the ladder
    void PlayerClimbLadder() { 
        if(CheckClimbLadder()==true)
        {
            if (!IsGrounded)
            {
                PlayerRigid2d.gravityScale = 0;
            }

            if (Input.GetKey(KeyCode.E))
            {
                PlayerRigid2d.MovePosition((Vector2)transform.position + Vector2.up * PlayerClimbSpeed * Time.deltaTime);
                
            }

        }

        else if(CheckClimbLadder()== false)
        {
            PlayerRigid2d.gravityScale = 10;
            
        }
        
     
    }

    //generic player movement
    private void PlayerMove()
    {

        

            if (IsGrounded) // if player is move on the ground with normal speed
            {
                if (inputH > 0)
                {
                    PlayerRigid2d.velocity = new Vector2(PlayerSpeed, PlayerRigid2d.velocity.y);

                }

                  else if (inputH<0)
                    {
                        PlayerRigid2d.velocity = new Vector2(-PlayerSpeed, PlayerRigid2d.velocity.y);

                    }
                    else
                    {//No key is pressed
                        PlayerRigid2d.velocity = new Vector2(0, PlayerRigid2d.velocity.y);
                      

                    }
                }

            
            else if(!IsGrounded)// if player is jumping we can have them some control
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
        
        
        if (IsWallGrab)
        {
            PlayerDoubleJump = true;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded) 
            {
                PlayerRigid2d.velocity =  Vector2.up * PlayerJumpPow;

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
        if (inputH <0  &&  FaceRight == true) { Flip(); }
        else if (inputH > 0 && FaceRight == false) { Flip(); }
        
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


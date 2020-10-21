using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public float PlayerSpeed; // for move left and right
    public float PlayerJumpPow, PlayerDoubleJumpPow; // for jump
    public float MidAirSpeed; // for move left and right while mid air
    [SerializeField] private LayerMask groundlayermask, walllayermask, ladderlayermask;


    [HideInInspector] public PlayerAttackTrigger playerAttack;
    [HideInInspector] public Animator PlayerAnimator;
    [HideInInspector] public Rigidbody PlayerRigid2d;

    float inputH; 
    
    bool isGrounded;
    
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

    [HideInInspector] public bool isKnockDown = false;
    [HideInInspector] public int getUpCount = 0;

    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;

    public Animator animator;
   
    void Awake()
    {
        playerAttack = transform.GetComponent<PlayerAttackTrigger>();
        PlayerRigid2d = transform.GetComponent<Rigidbody>();
        PlayerAnimator = transform.GetComponent<Animator>();
        boxCollider = transform.GetComponent<BoxCollider>();
        capsuleCollider = transform.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        CheckKnockDown();

        CheckPlayerGrounded();
       //CheckPlayerGrabWall();
        CheckPlayerBlock();



        if (!isPlayerBlock && !isKnockDown) { 
            PlayerJump();
            InputHorrizontal(); // Included player flip
            SetPlayerAnimator();// could change to call animation if needed
        }

      /*  if (Input.GetKey(KeyCode.Space))
        {
            print("jump");
            PlayerRigid2d.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * PlayerJumpPow, ForceMode.Impulse);
        }*/

    }

    void FixedUpdate()
    {

        if (!isPlayerBlock && !isKnockDown)// when player is not blocking they can move
        {
            PlayerMove();
         
            PlayerClimbWal();
          
            
        }

      
    }

    void CheckPlayerGrounded()
    {
        
        float distance = 3f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, distance, groundlayermask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

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

        FlipPlayer();
    }
       
    // check collide with wall 
   
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall")&& IsWallGrab==false)
        {
            IsWallGrab = true;          
        }
        print("collide wall");
    }

    private void OnCollisionExit(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("wall") && IsWallGrab == true)
        {
            IsWallGrab = false;
            
        }
        print("no wall");
    }
    
    //player climb on wall
    void PlayerClimbWal()
    {
        

        if (IsWallGrab == true)
        {
            
                if (Input.GetKey(KeyCode.E))// climb up
                {
                PlayerRigid2d.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * PlayerClimbSpeed, ForceMode.Impulse); ;
                    if (inputH != 0)
                    {
                        PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * Time.deltaTime);

                    
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


    //generic player movement
    private void PlayerMove()
    {
            if (isGrounded) // if player is move on the ground with normal speed
            {
                if (inputH != 0)
                {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * PlayerSpeed* Time.deltaTime);
                    animator.SetBool("IsRunning", true);
                }
   
            }
            else if(!isGrounded)// if player is jumping we can have them some control
            {
            if (inputH != 0)
            {
                PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH *MidAirSpeed* Time.deltaTime);
            }
        } 
        
    }


   
    private void PlayerJump() // both single and double 
        // side note could handle jump power by * with the character height. at the moment the vector in middle of the character so 7pixel long
    {
        if (isGrounded)
        {
            PlayerDoubleJump = true;
        }
        
        
        if (IsWallGrab)
        {
            PlayerDoubleJump = true;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) 
            {
                PlayerRigid2d.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * PlayerJumpPow, ForceMode.Impulse);
              
            }
            else

            if (PlayerDoubleJump==true)
            {
                PlayerRigid2d.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * PlayerDoubleJumpPow, ForceMode.Impulse);
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
        
        PlayerAnimator.SetBool("PlayerGrounded", isGrounded);
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
        //Scale.z *= -1;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerFrontPos.position, CheckRadius);
    }

    void CheckKnockDown()
    {
        if (isKnockDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            playerAttack.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space)) { getUpCount++; }   
                        
        }
    }
}


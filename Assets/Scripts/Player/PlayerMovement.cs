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
    [HideInInspector] public bool isBeingKnockedDown;
    [HideInInspector] public int getUpCount = 0;

    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;

    public Animator animator;

    public float slideSpeed;

    public bool isCollideWall;



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
        CheckCollideWall();
        CheckOnTop();
        InputHorrizontal(); // Included player flip

        if (isPlayerBlock)
        {
            animator.SetBool("Blocking", true);
        }
        else
        {
            animator.SetBool("Blocking", false);
        }

        if (!isPlayerBlock && !isKnockDown)
        {
            PlayerJump();
            //SetPlayerAnimator();// could change to call animation if needed
        }

    }

    void FixedUpdate()
    {

        if (!isPlayerBlock && !isKnockDown)// when player is not blocking they can move
        {
            PlayerMove();

        }
        if (isCollideWall && Input.GetKey(KeyCode.E))
        {
            PlayerClimbWal();
        }

    }

    void CheckPlayerGrounded()
    {
        
        float distance = 3f;
        

        if (Physics.Raycast(boxCollider.bounds.center, Vector3.down, distance, groundlayermask))
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
   
    
    void CheckCollideWall()
    {
        float distance = 2f;
        if (FaceRight)
        {
           isCollideWall=Physics.Raycast(transform.position, Vector3.right, distance, walllayermask);
            
        }
        else
        {
            isCollideWall=Physics.Raycast(transform.position, Vector3.left, distance, walllayermask);
        }
    }

    void CheckOnTop()
    {
        float distance = 4f;
        if (FaceRight)
        {
            isOnTop= (!Physics.Raycast(PlayerAbovePos.position , Vector3.right, distance, walllayermask)&& Physics.Raycast(PlayerUnderPos.position, Vector3.right, distance, walllayermask));

        }
        else
        {
            isOnTop = (!Physics.Raycast(PlayerAbovePos.position, Vector3.left, distance, walllayermask) && Physics.Raycast(PlayerUnderPos.position, Vector3.left, distance, walllayermask));
        }

    }
    
    //player climb on wall
    void PlayerClimbWal()
    {
        if (!isOnTop) 
        {
            PlayerRigid2d.velocity += ( new Vector3(0, 1, 0) * PlayerClimbSpeed  * Time.deltaTime);
        }
        else
        {
            PlayerRigid2d.velocity =(new Vector3(0, -1, 0) * slideSpeed * Time.deltaTime + Vector3.right* inputH*MidAirSpeed*Time.deltaTime);
            if (Input.GetKey(KeyCode.Space))
            {
                PlayerRigid2d.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * PlayerDoubleJumpPow, ForceMode.Impulse);

            }
        }
        
    }
      
    

    
 
    private void PlayerMove()
    {
        if (!isCollideWall)
        {
            if (isGrounded) // if player is move on the ground with normal speed
            {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * PlayerSpeed * Time.deltaTime);
                    // animator.SetBool("IsRunning", true);

            }
            else if (!isGrounded)// if player is jumping we can have them some control
            {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * MidAirSpeed * Time.deltaTime);
            }
        }
        if (isCollideWall)
        {
            if (isGrounded)
            {
                if (inputH * transform.localScale.x == -1)
                {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * PlayerSpeed * Time.deltaTime);
                }
            }
            else if ((!isGrounded))
            {
                if (inputH * transform.localScale.x == -1)
                {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * MidAirSpeed * Time.deltaTime);
                }
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
        Gizmos.DrawRay(boxCollider.bounds.center, Vector3.down);
    }

    void CheckKnockDown()
    {
        if (isBeingKnockedDown)
        {
            animator.SetBool("KnockDown", true);

            isBeingKnockedDown = false;
            isKnockDown = true;
        }
        if(isKnockDown && getUpCount >= 5)
        {
            animator.SetBool("BounceUp", true);
            isKnockDown = false;
            playerAttack.enabled = true;
        }
        else if (isKnockDown)
        {
            animator.SetBool("KnockedDown", true);

            playerAttack.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space)) { getUpCount++; }          
        }
    }
}


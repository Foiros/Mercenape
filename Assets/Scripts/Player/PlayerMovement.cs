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

    float inputH,inputV; 
    
    bool isGrounded;
    
    public bool FaceRight = true;
    public bool PlayerDoubleJump;

    public Transform PlayerFrontPos, PlayerBehindPos;
    public Transform PlayerUnderPos, PlayerAbovePos;
    public float CheckRadius;
    
    bool isOnTop;
    public float PlayerClimbSpeed;

    public bool isPlayerBlock = false;

    [HideInInspector] public bool isKnockDown = false;
    [HideInInspector] public int getUpCount = 0;

    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;

    public Animator animator;

    public float slideSpeed;
    public bool isCollideWall;

    bool isGrabWall = false;
    bool isJumping = false;


    void Awake()
    {
        playerAttack = transform.GetComponent<PlayerAttackTrigger>();
        PlayerRigid2d = transform.GetComponent<Rigidbody>();
        //PlayerAnimator = transform.GetComponent<Animator>();
        boxCollider = transform.GetComponent<BoxCollider>();
        capsuleCollider = transform.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        CheckKnockDown();

        CheckPlayerGrounded();
        //CheckPlayerGrabWall();       
        CheckCollideWall();
        CheckOnTop();
        CheckGrabWall();
     
        if (isGrounded || isCollideWall)
        {
            PlayerDoubleJump = true;
        }
        if (isGrounded)
        {
            isJumping = false;
        }

        if (!isKnockDown)
        {
            CheckPlayerBlock();
            InputHorrizontal(); // Included player flip
            InputVertical();
        }

        if (!isPlayerBlock && !isKnockDown)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
                PlayerJump();
            }  
        }

        if (isGrabWall == true)
        {
            PlayerRigid2d.useGravity = false;
            PlayerClimbWal();
        }
        else
        {
            PlayerRigid2d.useGravity = true;
        }

       
        SetAnimatorPara();

    }

    void FixedUpdate()
    {
        if (!isPlayerBlock && !isKnockDown)// when player is not blocking they can move
        {          
            PlayerMove();
        }     
    }

    void CheckPlayerGrounded()
    {      
        float distance = .5f;
        Debug.DrawRay(boxCollider.bounds.center, Vector3.down * distance, Color.red);

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
        if (!isGrounded) { return; }    // Don't check if player is on air

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

        if (inputH != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    void InputVertical()
    {
        inputV = Input.GetAxisRaw("Vertical");
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
        float distance = 2f;
        if (FaceRight)
        {
            isOnTop= (!Physics.Raycast(PlayerAbovePos.position , Vector3.right, distance, walllayermask)&& Physics.Raycast(PlayerUnderPos.position, Vector3.right, distance, walllayermask));
        }
        else
        {
            isOnTop = (!Physics.Raycast(PlayerAbovePos.position, Vector3.left, distance, walllayermask) && Physics.Raycast(PlayerUnderPos.position, Vector3.left, distance, walllayermask));
        }

    }

    void CheckGrabWall()
    {      
        if (isCollideWall)
        {
            if (isGrabWall == false)
            {
                if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.S))
                {
                    isGrabWall = true;
                }
            }
            else
            {
                if(Input.GetKey(KeyCode.E)&& Input.GetKey(KeyCode.S))
                {
                    isGrabWall = false;
                }
            }
        }
        if (!isCollideWall)
        {
            isGrabWall = false;
        }

    }

    //player climb on wall
    void PlayerClimbWal()
    {
        if (inputV == 0)
        {

            PlayerRigid2d.velocity = new Vector3(PlayerRigid2d.velocity.x, 0, 0);
        }
        else
        {
            if (!isOnTop)
            {
                PlayerRigid2d.velocity += (new Vector3(0, 1, 0) * PlayerClimbSpeed *inputV* Time.deltaTime);
            }
            else
            {
                PlayerRigid2d.velocity = new Vector3(PlayerRigid2d.velocity.x, 0, 0);
                if (Input.GetKey(KeyCode.Space))
                {
                    PlayerRigid2d.velocity += new Vector3(0.0f, 1.0f, 0.0f) * PlayerDoubleJumpPow;
                    PlayerDoubleJump = false;
                }
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
            PlayerRigid2d.velocity+= new Vector3(0.0f, 1.0f, 0.0f) * PlayerJumpPow;


        }
        else if( !isGrounded && PlayerDoubleJump )
            {
                
                    PlayerRigid2d.velocity+=new Vector3(0.0f, 1.0f, 0.0f) * PlayerDoubleJumpPow;
                    PlayerDoubleJump = false;

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
        Scale.z *= -1;
        //Scale.x *= -1;
        transform.localScale = Scale;
    }
   
    void CheckKnockDown()
    {             
        if (isKnockDown)
        {
            animator.SetBool("KnockedDown", true);

            playerAttack.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space)) { getUpCount++; }

            if (getUpCount >= 5)
            {
                getUpCount = 0;
                isKnockDown = false;
                
                animator.SetTrigger("BounceUp");
                animator.SetBool("KnockedDown", false);
                
                playerAttack.enabled = true;

            }
        }
    }

    void SetAnimatorPara()
    {
        animator.SetFloat("inputH", Mathf.Abs(inputH));
        animator.SetFloat("inputV", Mathf.Abs(inputV));
        animator.SetFloat("vSpeed", PlayerRigid2d.velocity.y);

        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", isGrounded);

        animator.SetBool("Blocking", isPlayerBlock);

        animator.SetBool("isGrabWall", isGrabWall);       
    }

}


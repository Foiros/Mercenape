﻿using System.Collections;
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

    float inputH, inputV;

    public bool isGrounded;

    public bool FaceRight = true;
    public bool PlayerDoubleJump;

    public Transform PlayerFrontPos, PlayerBehindPos;
    public Transform PlayerUnderPos, PlayerAbovePos;
    public float CheckRadius;

    public float PlayerClimbSpeed;

    public bool isPlayerBlock = false;

    [HideInInspector] public bool isKnockDown = false;
    [HideInInspector] public int getUpCount = 0;

    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;

    public Animator animator;

    public bool isCollideWall;

    public bool isGrabWall = false;
    public bool isJumping = false;

    GameObject bounceBackMessage;
    //Start Hash ID 
    [HideInInspector]
    public int isJumping_animBool,
        isGrounded_animBool,
        isGrabWall_animBool,
        knockedDown_animBool,
        blocking_animBool,
        isRunning_animBool;

    [HideInInspector]
    public int inputH_animFloat,
        inputV_animFloat,
        vSpeed_animafloat;

    //end Hash ID
    void Awake()
    {
        playerAttack = transform.GetComponent<PlayerAttackTrigger>();
        PlayerRigid2d = transform.GetComponent<Rigidbody>();
        //PlayerAnimator = transform.GetComponent<Animator>();
        boxCollider = transform.GetComponent<BoxCollider>();
        capsuleCollider = transform.GetComponent<CapsuleCollider>();
        PlayerRigid2d.centerOfMass = Vector3.zero;

        bounceBackMessage = GameObject.FindGameObjectWithTag("PlayerUI").transform.GetChild(4).gameObject;

        //HashID animator parameters for performances
        isJumping_animBool = Animator.StringToHash("isJumping");
        isGrounded_animBool = Animator.StringToHash("isGrounded");
        isGrabWall_animBool = Animator.StringToHash("isGrabWall");
        blocking_animBool = Animator.StringToHash("Blocking");
        knockedDown_animBool = Animator.StringToHash("KnockedDown");
        isRunning_animBool = Animator.StringToHash("IsRunning");
        inputH_animFloat = Animator.StringToHash("inputH");
        inputV_animFloat = Animator.StringToHash("inputV");
        vSpeed_animafloat = Animator.StringToHash("vSpeed");
    }

    void Update()
    {
        SetAnimatorPara();

        CheckKnockDown();
        CheckPlayerGrounded();
        CheckCollideWall();
        CheckOnTop();
        CheckGrabWall();

        if (isGrounded)
        {
            PlayerDoubleJump = true;
            isJumping = false;
        }
        else
        {
            isJumping = true;
            isPlayerBlock = false;
        }

        if (!isKnockDown)
        {
            CheckPlayerBlock();
            InputHorrizontal(); // Included player flip
            InputVertical();
        }

        if (!isKnockDown && !CheckColliderAbove())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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

        if (isGrabWall && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            isPlayerBlock = false;
            isGrabWall = false;
            PlayerRigid2d.velocity += (Vector3.up * PlayerJumpPow + Vector3.right);
        }


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
        float distance = 1f;
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

        if (inputH != 0 && !isPlayerBlock)
        {
            //animator.SetBool("IsRunning", true);
            animator.SetBool(isRunning_animBool, true);
        }
        else
        {
            //animator.SetBool("IsRunning", false);
            animator.SetBool(isRunning_animBool, false);
        }
    }

    bool CheckColliderAbove()
    {
        float distance = 2f;
        Debug.DrawRay(PlayerAbovePos.position, Vector3.up * distance, Color.yellow);
        return Physics.Raycast(PlayerAbovePos.position, Vector3.up, distance, walllayermask);

    }


    void InputVertical()
    {
        inputV = Input.GetAxisRaw("Vertical");
    }

    // check collide with wall  
    void CheckCollideWall()
    {
        float distance = 1.5f;
        if (FaceRight)
        {
            isCollideWall = Physics.Raycast(capsuleCollider.bounds.center, Vector3.right, distance, walllayermask);
            Debug.DrawRay(capsuleCollider.bounds.center, Vector3.right * distance, Color.yellow);
        }
        else
        {
            isCollideWall = Physics.Raycast(capsuleCollider.bounds.center, Vector3.left, distance, walllayermask);
            Debug.DrawRay(capsuleCollider.bounds.center, Vector3.left * distance, Color.yellow);

        }
    }

    bool CheckOnTop()
    {
        float distance = 1.5f;
        if (FaceRight)
        {
            Debug.DrawRay(PlayerAbovePos.position, Vector3.right * distance, Color.yellow);
            return (!Physics.Raycast(PlayerAbovePos.position, Vector3.right, distance, walllayermask) && Physics.Raycast(PlayerUnderPos.position, Vector3.right, distance, walllayermask));


        }
        else
        {
            Debug.DrawRay(PlayerAbovePos.position, Vector3.left * distance, Color.yellow);
            return (!Physics.Raycast(PlayerAbovePos.position, Vector3.left, distance, walllayermask) && Physics.Raycast(PlayerUnderPos.position, Vector3.left, distance, walllayermask));

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
                    isJumping = false;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.S))
                {
                    isGrabWall = false;
                    isJumping = true;
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
        float xSpeed = 0.5f;

        if (inputV == 0)
        {

            PlayerRigid2d.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            if (!CheckOnTop())
            {
                if (inputV > 0)
                {
                    PlayerRigid2d.velocity += (new Vector3(0, 1, 0) * PlayerClimbSpeed * inputV * Time.deltaTime + Vector3.right * xSpeed * Time.deltaTime);
                }
                else if (inputV < 0)
                {
                    PlayerRigid2d.velocity += (new Vector3(0, 1, 0) * PlayerClimbSpeed * inputV * Time.deltaTime + Vector3.right * -xSpeed * Time.deltaTime);
                }

                if (isGrounded && inputV < 0)
                {
                    isGrabWall = false;
                }
            }
            else
            {
                if (inputV > 0)
                {
                    PlayerRigid2d.velocity = new Vector3(0, 0, 0);
                }
                else if (inputV < 0)
                {
                    PlayerRigid2d.velocity += (new Vector3(0, 1, 0) * PlayerClimbSpeed * inputV * Time.deltaTime + Vector3.right * -xSpeed * Time.deltaTime);

                }

                if (Input.GetKey(KeyCode.Space))
                {
                    PlayerRigid2d.velocity += new Vector3(0.0f, 1.0f, 0.0f) * PlayerDoubleJumpPow;
                    PlayerDoubleJump = false;
                }
            }
        }
    }


    void OffsetCapsulCollider()
    {
        capsuleCollider.radius = 0.5f;
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
                if (inputH * transform.localScale.z == -1)
                {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * PlayerSpeed * Time.deltaTime);
                }
            }
            else if ((!isGrounded))
            {
                if (inputH * transform.localScale.z == -1)
                {
                    PlayerRigid2d.MovePosition((Vector3)transform.position + Vector3.right * inputH * MidAirSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void PlayerJump() // both single and double 
                              // side note could handle jump power by * with the character height. at the moment the vector in middle of the character so 7pixel long
    {
        if (IsBouncingBack()) { return; }

        if (isGrounded)
        {
            animator.SetTrigger("JumpStart");
            PlayerRigid2d.velocity += new Vector3(0.0f, 1.0f, 0.0f) * PlayerJumpPow;
            isJumping = true;
            isPlayerBlock = false;

        }
        else if (!isGrounded && PlayerDoubleJump)
        {
            //animator.SetTrigger("JumpStart");
            if (PlayerRigid2d.velocity.y <= 0.1)
            {
                PlayerRigid2d.velocity = Vector3.zero;
            }
            PlayerRigid2d.velocity += new Vector3(0.0f, 1.0f, 0.0f) * PlayerDoubleJumpPow;
            PlayerDoubleJump = false;
            isJumping = true;
            isPlayerBlock = false;
        }
    }


    private void FlipPlayer()
    {
        if (inputH < 0 && FaceRight == true) { Flip(); }
        else if (inputH > 0 && FaceRight == false) { Flip(); }
    }

    private void Flip()
    {
        FaceRight = !FaceRight;
        Vector3 Scale = transform.localScale;
        Scale.z *= -1;
        //Scale.x *= -1;
        transform.localScale = Scale;
    }

    void CheckKnockDown()
    {
        if (isKnockDown)
        {
            isGrabWall = false;

            animator.SetLayerWeight(1, 0.1f);
            animator.SetBool(knockedDown_animBool, true);
            playerAttack.enabled = false;
            if (bounceBackMessage != null) { bounceBackMessage.SetActive(true); }
            
            if (Input.GetKeyDown(KeyCode.Space)) { getUpCount++; }
        }
        else
        {
            if (bounceBackMessage != null) { bounceBackMessage.SetActive(false); }
            animator.SetLayerWeight(1, 1f);
        }
    }

    public void PlayerBounceUp()
    {
        getUpCount = 0;
        isKnockDown = false;

        animator.SetTrigger("BounceUp");

        playerAttack.enabled = true;
    }

    public bool IsBouncingBack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("BounceBack");
    }

    void SetAnimatorPara()
    {
        animator.SetFloat(inputH_animFloat, Mathf.Abs(inputH));
        animator.SetFloat(inputV_animFloat, Mathf.Abs(inputV));
        animator.SetFloat(vSpeed_animafloat, PlayerRigid2d.velocity.y);

        animator.SetBool(isJumping_animBool, isJumping);
        animator.SetBool(isGrounded_animBool, isGrounded);
        animator.SetBool(blocking_animBool, isPlayerBlock);
        animator.SetBool(isGrabWall_animBool, isGrabWall);
        animator.SetBool(knockedDown_animBool, isKnockDown);
    }

    


}


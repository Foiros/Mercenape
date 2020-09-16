using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float PlayerSpeed, PlayerMaxSpeed, PlayerJumpPow;
    [SerializeField] private LayerMask groundlayermask;

    public Animator PlayerAnimator;
    public Rigidbody2D PlayerRigid2d;
    public BoxCollider2D PlayerboxCollider2d;
    public bool FaceRight = true;

  



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
        SetPlayerAnimator();
    }

   

    private void PlayerMove()
    {

        PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (Input.GetKey(KeyCode.D) )
        {
            PlayerRigid2d.velocity = new Vector2(PlayerSpeed, PlayerRigid2d.velocity.y); ;

        }
        else
        {
            if (Input.GetKey(KeyCode.A) )
                {
                PlayerRigid2d.velocity = new Vector2(-PlayerSpeed,PlayerRigid2d.velocity.y);

            }
            else
            {//No key is pressed
                PlayerRigid2d.velocity = new Vector2(0, PlayerRigid2d.velocity.y);
                PlayerRigid2d.constraints = RigidbodyConstraints2D.FreezePositionX;

            }
        }
        }
    private void PlayerJump()
    {
        if ( PlayerGrounded() && Input.GetKey(KeyCode.W)){
            PlayerRigid2d.velocity = Vector2.up * PlayerJumpPow;
        }

    }
    
    public bool PlayerGrounded() // check if player is on the ground
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(PlayerboxCollider2d.bounds.center, PlayerboxCollider2d.bounds.size, 0f, Vector2.down, .1f,groundlayermask);
        return raycastHit2D.collider != null;
    }

    private void SetPlayerAnimator()
    {
        bool IsGrounded = PlayerGrounded();
        PlayerAnimator.SetBool("PlayerGrounded", IsGrounded);
        PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(PlayerRigid2d.velocity.x));
        
    }

    public void FlipPlayer()
    {
        if (PlayerRigid2d.velocity.x < 0 && FaceRight == true) { Flip(); }
        if (PlayerRigid2d.velocity.x > 0 && FaceRight == false) { Flip(); }
    }

    public void Flip()
    {
        FaceRight = !FaceRight;
        Vector2 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

    }

 

}


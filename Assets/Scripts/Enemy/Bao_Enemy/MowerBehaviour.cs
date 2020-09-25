using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerBehaviour : EnemyStat
{
    private Coroutine coroutine;

    private bool isAttacking = false;
    private bool isRiding = false;

    private float ridePos;

    private Transform backSide;

    protected override void Start()
    {
        base.Start();   // Start both EnemyStat and MowerBehaviour       

        backSide = transform.GetChild(2);
    }

    void Update()
    {
        StunningProcess();

        if (isRiding)
        {
            player.transform.position = new Vector2(transform.position.x +  ridePos, player.transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ridePos = player.transform.position.x - transform.position.x;
            isRiding = true;          
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {          
            isRiding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!isAttacking)
            {
                isAttacking = true;

                MowerAttack();
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (!collision.gameObject.CompareTag("Player"))
        {
            isRiding = false;
        }
    }

    private void MowerAttack()
    {
        playerRigid.velocity = Vector2.zero;

        escapingStunCount = 0;
        isStunning = true;

        StartCoroutine("Attacking");
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ApplyDamage(3, damage));
    }

    protected override void StunningProcess()
    {
        base.StunningProcess();     // Still normal stun player

        if (escapingStunCount == 8)
        {
            // Stop dealing damage and get back to original states
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMovement.enabled = true;
            player.GetComponent<PlayerAttackTrigger>().enabled = true;
            player.GetComponent<Animator>().enabled = true;

            playerRigid.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * 3000, 0));

            escapingStunCount = 0;
            readyToSetStun = true;
            isStunning = false;

            Invoke("ReturnPhysics", 0.5f);
        }        
    }

    void ReturnPhysics()
    {
        boxCollier.isTrigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        speed = runningSpeed;
        isAttacking = false;
    }

    IEnumerator Attacking()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollier.isTrigger = true;
        speed = 1f;

        yield return new WaitForSeconds(3f);

        ReturnPhysics();      
    }

    // Deal damage
    IEnumerator ApplyDamage(int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while (currentCount < damageCount)
        {
            playerStat.PlayerHP -= damageAmount;
            yield return new WaitForSeconds(1f);
            currentCount++;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerBehaviour : EnemyStat
{
    private IEnumerator coroutine;

    protected override void Start()
    {
        base.Start();   // Start both EnemyStat and MowerBehaviour       

        coroutine = ApplyDamage(3, damage);
    }

    void Update()
    {
        StunningProcess();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            MowerAttack();
        }
    }

    private void MowerAttack()
    {
        StartCoroutine("Attacking");

        escapingStunCount = 0;
        isStunning = true;

        StopCoroutine(coroutine);
        StartCoroutine(coroutine);
    }

    protected override void StunningProcess()
    {
        base.StunningProcess();     // Still normal stun player

        if (escapingStunCount == 8)
        {
            // Stop dealing damage and get back to original states
            StopCoroutine(coroutine);

            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMovement.enabled = true;
            player.GetComponent<PlayerAttackTrigger>().enabled = true;
            player.GetComponent<Animator>().enabled = true;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * 3000, 0));

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

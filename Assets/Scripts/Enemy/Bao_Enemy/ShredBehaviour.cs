using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Bao: Shred's Behaviour, child of EnemyBehaviour
public class ShredBehaviour : EnemyBehaviour
{      
    [SerializeField] private float bleedChance;
    [SerializeField] private int knockBackForce;

    private Coroutine co;

    protected override void Start()
    {
        base.Start();   // Start both EnemyBehaviour and ShredBehaviour       
    }

    private void Update()
    {
        // Check if player is stunned by Shred
        StunningProcess();

        // Temporary cheat code to kill all ememy
        if (Input.GetKeyDown(KeyCode.L))
        {
            Destroy(gameObject);
        }
    }

    // Hit player
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!playerMovement.isPlayerBlock) // if player is not blocking, attack player normal
            {
                ShredAttack();
            }
            else // when player is blocking
            {
                if (IsFacingRight() == playerMovement.FaceRight) // if hit player from behind aka both face same direction then atk player normally
                {
                    ShredAttack();
                }
                else
                {
                    StopCoroutine("ImmobilizeShred");
                    StartCoroutine("ImmobilizeShred");
                }
            }           
        }
    }

    // When Shred attacks and applies damage
    private void ShredAttack()
    {
        StartCoroutine("Attacking");
        playerStat.PlayerTakeDamage(stat.damage);
        escapingStunCount = 0;
        isStunning = true;

        if (Random.Range(0f, 100f) < bleedChance)
        {
            if (co != null)
            {
                StopCoroutine(co);
            }
            co = StartCoroutine(ApplyBleedDamage(1, 3, 2));
        }
    }

    // Make sure Shred pass through player after attaking
    IEnumerator Attacking()
    {
        // Pass through player
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollier.isTrigger = true;  
        
        yield return new WaitForSeconds(0.7f);

        // Return to original states
        boxCollier.isTrigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;       
    }

    // Do bleed damage (per sec for now)
    IEnumerator ApplyBleedDamage(float damageDuration, int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while(currentCount < damageCount)
        {
            playerStat.PlayerHP -= damageAmount;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }       
    }
   
    IEnumerator ImmobilizeShred()
    {
        //playerRigid.AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * knockBackForce / 50, 0));
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * knockBackForce, 50));

        speed = 0;       

        yield return new WaitForSeconds(1f);

        speed = stat.runningSpeed;
    }

    protected override void StunningProcess()
    {
        base.StunningProcess();     // Still normally stun player

        if (escapingStunCount == 5)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMovement.enabled = true;
            player.GetComponent<PlayerAttackTrigger>().enabled = true;
            player.GetComponent<Animator>().enabled = true;          

            escapingStunCount = 0;
            readyToSetStun = true;
            isStunning = false;
        }
    }

    public override void TakeDamage(float playerDamage)
    {
        base.TakeDamage(playerDamage);
    }
}



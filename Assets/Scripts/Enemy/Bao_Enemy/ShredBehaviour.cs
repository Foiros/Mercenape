using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredBehaviour : EnemyStat
{   
    
    [SerializeField] private float bleedChance;
    [SerializeField] private int knockBackForce;

    protected override void Start()
    {
        base.Start();   // Start both EnemyStat and ShredBehaviour       
    }

    private void Update()
    {
        // Check if is stunning
        StunningProcess();

        // Temporary cheat code to kill all ememy
        if (Input.GetKeyDown(KeyCode.L))
        {
            Destroy(gameObject);
        }
    }

    // Hit player
    private void OnCollisionEnter2D(Collision2D col)
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
        playerStat.PlayerTakeDamage(damage);
        escapingStunCount = 0;
        isStunning = true;

        if (Random.Range(0f, 100f) < bleedChance)
        {
            StopCoroutine(ApplyBleedDamage(1, 3, 2));
            StartCoroutine(ApplyBleedDamage(1, 3, 2));
        }
    }

    // Make sure Shred pass through player after attaking
    IEnumerator Attacking()
    {
        // Pass through player
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollier.isTrigger = true;      

        yield return new WaitForSeconds(1.0f);

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
        //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * knockBackForce / 50, 0));
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * knockBackForce, 50));

        speed = 0;
        this.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1f);

        speed = runningSpeed;
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected override void StunningProcess()
    {
        base.StunningProcess();     // Still normal stun player

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
}



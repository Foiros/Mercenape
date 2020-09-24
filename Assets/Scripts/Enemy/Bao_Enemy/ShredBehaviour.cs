using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredBehaviour : EnemyStat
{
    [SerializeField] private float runningSpeed = 10f;
    private float speed;
    private int escapingStunCount = 0;
    [SerializeField] private float bleedChance;
    [SerializeField] private int knockBackForce;

    bool isStunning = false;        // For player is stunning
    bool readyToSetStun = true;     // For stunning process
  
    private BoxCollider2D boxCollier;
    //private CapsuleCollider2D capsuleCollider;    // For detecting ground

    private GameObject player;
    private PlayerStat playerStat;
    private PlayerMovement playerMovement;

    protected override void Start()
    {
        base.Start();   // Start both EnemyStat and ShredBehaviour

        speed = runningSpeed;
       
        boxCollier = GetComponent<BoxCollider2D>();
        //capsuleCollider = GetComponent<CapsuleCollider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<PlayerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();

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

    // Movement
    void FixedUpdate()
    {
        // Check direction facing and adjust to velocity according to that, also rotate Health bar
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(speed, 0f);
            healthBarUI.transform.localScale = new Vector2(xScaleUI, healthBarUI.transform.localScale.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0f);
            healthBarUI.transform.localScale = new Vector2(-xScaleUI, healthBarUI.transform.localScale.y);
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
   
    // Detect if Shred gets out of the ground, turn if yes
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) // Fix a bug that Shred stick to player when colliding
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * 0.3f, 0.3f);
        }
    }

    // Check if facing right direction
    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
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

    // Set player movement when being knocked down
    private void StunningProcess()
    {
        if (!isStunning) { return; }

        if (readyToSetStun) // Make sure just to set this one time (for performance and bugs fixing)
        {
            readyToSetStun = false;

            player.transform.rotation = Quaternion.Euler(0, 0, Mathf.Sign(transform.localScale.x) * -90);
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttackTrigger>().enabled = false;
            player.GetComponent<Animator>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            escapingStunCount++;
        }

        if (escapingStunCount == 5)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerAttackTrigger>().enabled = true;
            player.GetComponent<Animator>().enabled = true;

            escapingStunCount = 0;
            readyToSetStun = true;
            isStunning = false;
        }
    }

    IEnumerator ImmobilizeShred()
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * knockBackForce, 0));
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * knockBackForce, 300));

        speed = 0;
        this.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1f);

        speed = runningSpeed;
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}



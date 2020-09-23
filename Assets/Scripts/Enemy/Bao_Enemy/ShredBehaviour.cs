using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public int enemyDamage;
    private int escapingStunCount = 0;
    [SerializeField] private float bleedChance;

    bool isStunning = false;    // For player is stunning
    bool readyToSetStun = true;               // For stunning process

    private Rigidbody2D rb;
    private BoxCollider2D boxCollier;
    //private CapsuleCollider2D capsuleCollider;    // For detecting ground

    private EnemyStat enemyStat;

    private GameObject player;
    private PlayerStat playerStat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollier = GetComponent<BoxCollider2D>();
        //capsuleCollider = GetComponent<CapsuleCollider2D>();

        enemyStat = this.GetComponent<EnemyStat>();
        enemyDamage = enemyStat.damage;

        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check direction facing and adjust to velocity according to that
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0f);
        }

        // Check if is stunning
        StunningProcess();
    }

    // Hit player
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {            
            StartCoroutine("Attacking");
            escapingStunCount = 0;
            isStunning = true;
            
            if (Random.Range(0f, 100f) < bleedChance)
            {
                print("Start bleeding");
                StopCoroutine(ApplyBleedDamage(1, 3, 2));
                StartCoroutine(ApplyBleedDamage(1, 3, 2));
            }
        }
    }
   
    // Detect if Shred gets out of the ground, turn if yes
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) // Fix a bug that Shred stick to player when colliding
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * 0.2f, 0.2f);
        }
    }

    // Check if facing right direction
    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    IEnumerator Attacking()
    {
        // Pass through player
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollier.isTrigger = true;      

        yield return new WaitForSeconds(1f);

        // Return to original states
        boxCollier.isTrigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;       
    }

    IEnumerator ApplyBleedDamage(float damageDuration, int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while(currentCount < damageCount)
        {
            playerStat.PlayerHP -= damageAmount;
            print("Player HP: " + playerStat.PlayerHP);
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
        print("End bleeding");
    }

    private void StunningProcess()
    {
        if (!isStunning) { return; }

        if (readyToSetStun) // Make sure just to set this one time (for performance)
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
            print("Pressed space: " + escapingStunCount);
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
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    [SerializeField] protected EnemyStats stat;

    protected GameObject healthBarUI;
    protected EnemyHealthBar barHealth;

    protected float speed;
    [SerializeField] protected float currentHP;

    protected bool isStunning = false;        // For player is stunning
    protected bool readyToSetStun = true;     // For stunning process
    protected int escapingStunCount = 0;      // Current number of pressing space

    private float enemyScale;
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollier;
    [SerializeField] protected Transform frontDetection;
    protected RaycastHit2D groundInfo;

    protected GameObject player;
    protected PlayerStat playerStat;
    protected PlayerMovement playerMovement;
    protected Rigidbody2D playerRigid;

    private EnemyLootDrop enemyLoot;

    private void Awake()
    {       
        enemyScale = transform.localScale.x;
    }

    protected virtual void Start()
    {
        healthBarUI = transform.GetChild(1).gameObject;
        barHealth = healthBarUI.GetComponent<EnemyHealthBar>();

        rb = GetComponent<Rigidbody2D>();
        boxCollier = GetComponent<BoxCollider2D>();

        //var waveStat = GameObject.Find("EnemySpawner");
        //maxHP += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedHP;
        //damage += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedDamage;
        speed = stat.runningSpeed;
        currentHP = stat.maxHP;
        barHealth.UpdateHealthBar(currentHP, stat.maxHP);

        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<PlayerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRigid = player.GetComponent<Rigidbody2D>();

        enemyLoot = GetComponent<EnemyLootDrop>();
    }

    // Movement
    protected void FixedUpdate()
    {
        Movement();
    }

    // Check if facing right direction
    protected bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    // Basic Movement
    protected virtual void Movement()
    {
        groundInfo = Physics2D.Raycast(frontDetection.position, Vector2.down, 10, LayerMask.GetMask("Ground"));

        if(groundInfo.collider == false)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * enemyScale, enemyScale);
            barHealth.ScaleRightUI(rb);           
        }

        // Check direction facing and adjust to velocity according to that
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    // Fix a bug that enemy stick to something when colliding
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {     

    }

    // Take damage from player
    public virtual void TakeDamage(float playerDamage)
    {
        currentHP -= playerDamage;

        barHealth.UpdateHealthBar(currentHP, stat.maxHP);

        CheckEnemyDeath();
    }

    // Set player movement when being knocked down
    protected virtual void StunningProcess()
    {
        if (!isStunning) { return; }

        if (readyToSetStun) // Make sure just to set this one time (for performance and bugs fixing)
        {
            readyToSetStun = false;

            player.transform.rotation = Quaternion.Euler(0, 0, Mathf.Sign(transform.localScale.x) * -90);
            playerMovement.enabled = false;
            player.GetComponent<PlayerAttackTrigger>().enabled = false;
            player.GetComponent<Animator>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            escapingStunCount++;
        }
    }

    protected void CheckEnemyDeath()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject, 0);

            enemyLoot.DropAll();
        }
    }
}

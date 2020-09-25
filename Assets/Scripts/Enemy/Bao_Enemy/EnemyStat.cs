using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    public Slider sliderHealth;
    public GameObject healthBarUI;
    protected float xScaleUI;   // For showing right

    [HideInInspector] public int currentHP;   // Player is accessing this
    [SerializeField] protected int maxHP;
    [SerializeField] protected int damage;

    protected bool isStunning = false;        // For player is stunning
    protected bool readyToSetStun = true;     // For stunning process
    protected int escapingStunCount = 0;      // Current number of pressing space

    [SerializeField] protected float runningSpeed = 10f;
    protected float speed;

    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollier;

    protected GameObject player;
    protected PlayerStat playerStat;
    protected PlayerMovement playerMovement;

    private EnemyLootDrop enemyLoot;

    private void Awake()
    {
        xScaleUI = healthBarUI.transform.localScale.x;
    }

    protected virtual void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        boxCollier = GetComponent<BoxCollider2D>();

        var waveStat = GameObject.Find("EnemySpawner");
        maxHP += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedHP;
        damage += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedDamage;
        currentHP = maxHP;
        speed = runningSpeed;

        sliderHealth.value = CalculateHealth();

        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<PlayerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();

        enemyLoot = GetComponent<EnemyLootDrop>();
    }

    // Movement
    protected void FixedUpdate()
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
    }

    // Check if facing right direction
    protected bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    // Detect if enemy gets out of the ground, turn if yes and also rotate Health bar
    protected void OnTriggerExit2D(Collider2D collision)
    {
        // Fix a bug that Shred stick to player when colliding
        if (collision.gameObject.CompareTag("Loot")) { return; }

        if (!collision.gameObject.CompareTag("Player"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * 0.3f, 0.3f);
            healthBarUI.transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * xScaleUI, healthBarUI.transform.localScale.y);
        }

    }

    // Take damage from player
    public void TakeDamage(int playerDamage)
    {
        currentHP -= playerDamage;
        print("Current HP: " + currentHP);
        
        sliderHealth.value = CalculateHealth();
        StartCoroutine("HealthBarAnimation");

        CheckEnemydeath();     
    }

    private float CalculateHealth()
    {
        return (float) currentHP / maxHP;
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

    IEnumerator HealthBarAnimation()
    {
        sliderHealth.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        sliderHealth.gameObject.SetActive(false);
    }
   
    private void CheckEnemydeath()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject, 0);

            enemyLoot.DropAll();

        }
    }

    
}

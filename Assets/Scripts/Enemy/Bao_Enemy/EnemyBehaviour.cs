using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Bao: main enemy behaviour (maybe change name later), parent of ShredBehaviour and MowerBehaviour
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected EnemyStats stat;

    protected GameObject healthBarUI;
    protected EnemyHealthBar barHealth;

    protected float speed;
    [SerializeField] protected float currentHP;

    protected bool isAttacker = false;    // For stunning process

    private Vector3 enemyRotation;
    protected Rigidbody rb;
    protected BoxCollider boxCollier;
    [SerializeField] protected Transform frontDetection;
    protected bool groundInfo;
    protected bool wallInfo;

    protected GameObject player;
    protected PlayerStat playerStat;
    protected PlayerMovement playerMovement;

    private EnemyLootDrop enemyLoot;

    private void Awake()
    {       
        enemyRotation = transform.rotation.eulerAngles;

        healthBarUI = transform.GetChild(1).gameObject;
        barHealth = healthBarUI.GetComponent<EnemyHealthBar>();

        rb = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();
    }

    protected virtual void Start()
    {
        Invoke("FreezePosY", 0.8f);

        //var waveStat = GameObject.Find("EnemySpawner");
        //maxHP += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedHP;
        //damage += waveStat.GetComponent<EnemySpawnerScript>().wave.enemyIncreasedDamage;
        speed = stat.runningSpeed;
        currentHP = stat.maxHP;
        barHealth.UpdateHealthBar(currentHP, stat.maxHP);

        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<PlayerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();      

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
        return transform.rotation.eulerAngles.y == (int)0;
    }

    // Basic Movement
    protected virtual void Movement()
    {
        if (currentHP <= 0) { return; }

        groundInfo = Physics.Raycast(frontDetection.position, Vector3.down, 10f, LayerMask.GetMask("Ground"));
        wallInfo = Physics.Raycast(frontDetection.position, transform.right, 2f, LayerMask.GetMask("Wall"));
       
        if (!groundInfo || wallInfo)
        {
            enemyRotation += new Vector3(0, -(Mathf.Sign(rb.velocity.x)) * 180, 0);
            transform.rotation = Quaternion.Euler(0, enemyRotation.y, 0);
            barHealth.ScaleRightUI(rb);
        }

        // Check direction facing and adjust to velocity according to that
        if (IsFacingRight())
        {          
            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
        }      
    }

    // Fix a bug that enemy stick to something when colliding
    protected virtual void OnTriggerExit(Collider collision)
    {     

    }
    
    // Take damage from player
    public virtual void TakeDamage(float playerDamage)
    {
        currentHP -= playerDamage;

        barHealth.UpdateHealthBar(currentHP, stat.maxHP);

        StopCoroutine("CheckEnemyDeath");
        StartCoroutine("CheckEnemyDeath");
    }

    // Process when player get knocked down, mainly in Shred and Mower script
    protected virtual void KnockDownProcess()
    {
        if (!playerMovement.isBeingKnockedDown) { return; }
    }

    protected IEnumerator CheckEnemyDeath()
    {
        // If dead
        if (currentHP <= 0)
        {
            rb.constraints = RigidbodyConstraints.None;
            transform.rotation = Quaternion.Euler(90, Random.Range(0f, 360f), 0);
            speed = 0;
            rb.useGravity = false;
            boxCollier.enabled = false;

            yield return new WaitForSeconds(1f);

            Destroy(gameObject, 0);

            enemyLoot.DropAll();
        }
    }

    protected void FreezePosY()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
}

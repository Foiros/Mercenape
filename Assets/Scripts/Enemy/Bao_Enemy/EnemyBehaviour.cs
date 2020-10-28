using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Bao: main enemy behaviour (maybe change name later), parent of ShredBehaviour and MowerBehaviour
// Edited by Arttu Paldán on 23.10.2020: Added an virtual function for player to add bleed on enemies. 
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected EnemyStats stat;

    protected GameObject healthBarUI;
    protected EnemyHealthBar barHealth;

    protected float speed;
    [SerializeField] protected float currentHP;

    protected bool isAttacker = false; // Make sure not all enemy activate function
    
    protected float weaponBleedDamage, weaponBleedDuration;
    protected int bleedTicks, currentBleedTicks;

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

        // Subscribe to the OnHitEnemy event in PlayerAttack
        playerMovement.playerAttack.OnHitEnemy += EnemyGetHit;
        playerMovement.playerAttack.OnBleedEnemy += ApplyBleeding;

        enemyLoot = GetComponent<EnemyLootDrop>();
    }

    // When enemy die, unsubscibe events
    protected void OnDisable()
    {
        // Not to cause error
        playerMovement.playerAttack.OnHitEnemy -= EnemyGetHit;
        playerMovement.playerAttack.OnBleedEnemy -= ApplyBleeding;
    }

    // Movement
    protected void FixedUpdate()
    {
        Movement();
    }

    // Check if facing right direction
    protected bool IsFacingRight()
    {
        return (int)transform.rotation.eulerAngles.y == 0;
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
            barHealth.ScaleLeftUI(rb);
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

        // If dead
        if (currentHP <= 0)
        {
            StopCoroutine("CheckEnemyDeath");
            StartCoroutine("CheckEnemyDeath");
        }
    }

    // Take bleed damage from player's weapon
    public void TakeBleedDammage(float weaponBleedDmg)
    {
        currentHP -= weaponBleedDmg;

        // If dead
        if (currentHP <= 0)
        {
            StopCoroutine("CheckEnemyDeath");
            StartCoroutine("CheckEnemyDeath");
        }
    }

    // Check to see which type of enemy get hit
    protected virtual void EnemyGetHit(bool isMowerBackSide, bool isMowerGenerator, Collider selfCol, float playerDmg)
    {
        if (!IsSelf(selfCol)) { return; }

        // Then deal damage to the correct enemy
        TakeDamage(playerDmg);
    }

    // Process when player get knocked down, mainly in Shred and Mower script
    protected virtual void KnockDownProcess()
    {
        if (!playerMovement.isKnockDown) { return; }
    }

    // Knock down player with animation
    protected void KnockPlayerDown()
    {
        if (!playerMovement.animator.GetCurrentAnimatorStateInfo(0).IsTag("KnockedDown"))
        {
            playerMovement.animator.SetTrigger("KnockDown");
            isAttacker = true;
        }

        playerMovement.getUpCount = 0;
        playerMovement.isKnockDown = true;
    }

    protected IEnumerator CheckEnemyDeath()
    {
        speed = 0;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        boxCollier.enabled = false;

        yield return new WaitForSeconds(1f);

        Destroy(gameObject, 0);

        enemyLoot.DropAll();
    }

    protected void FreezePosY()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    public virtual void ApplyBleeding(float damage, float duration, int ticks, Collider selfCol) 
    {
        if (!IsSelf(selfCol)) { return; }

        weaponBleedDamage = damage;
        weaponBleedDuration = duration;
        bleedTicks = ticks;
        currentBleedTicks = 1;

        // Stop stacking bleed before begin new bleed
        StopCoroutine(BleedTick());
        StartCoroutine(BleedTick());
    }

    IEnumerator BleedTick()
    {
        print("bleeding");
        while (currentBleedTicks <= bleedTicks)
        {
            TakeBleedDammage(weaponBleedDamage);
            yield return new WaitForSeconds(weaponBleedDuration);
            currentBleedTicks++;
        }
    }

    // Check to make sure only the enemy get hit is called, not every enemy
    protected bool IsSelf(Collider selfCol)
    {
        // Get root because Mower's backside and generator are not actually Mower itself
        return selfCol.gameObject.transform.root.gameObject.GetInstanceID() == this.gameObject.GetInstanceID();
    }
}

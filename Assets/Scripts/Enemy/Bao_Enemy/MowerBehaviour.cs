using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Bao: Mower's Behaviour, child of EnemyBehaviour
public class MowerBehaviour : EnemyBehaviour
{
    [SerializeField] private EnemyStats fieldStat;
    private float fieldHP;
    private SpriteRenderer fieldSprite;

    private GameObject fieldHealthBarUI;
    private EnemyHealthBar fieldBarHealth;

    private enum ForceFieldState { Inactive, Generating, Active, Destroyed }
    private ForceFieldState currentState;

    private Coroutine dmgCoroutine;

    private bool isAttacking = false;
    private bool isRiding = false;
    private bool isGenerating = false;

    private float ridePos;
    [SerializeField] private Transform rideHeight;

    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D generatorCollider;

    protected override void Start()
    {
        base.Start();   // Start both EnemyBehaviour and MowerBehaviour   

        currentState = ForceFieldState.Inactive;

        fieldHealthBarUI = transform.GetChild(2).GetChild(0).gameObject;
        fieldBarHealth = fieldHealthBarUI.GetComponent<EnemyHealthBar>();

        fieldHP = fieldStat.maxHP;
        fieldBarHealth.UpdateHealthBar(fieldHP, fieldStat.maxHP);

        fieldSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();

        capsuleCollider = transform.GetChild(2).GetComponent<CapsuleCollider2D>();
        generatorCollider = transform.GetChild(2).GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        // Check if player is knocked down by Mower
        KnockDownProcess();

        // If is riding, stick on the top of Mower
        if (isRiding)
        {
            player.transform.position = new Vector2(transform.position.x +  ridePos, player.transform.position.y);
        }

        switch (currentState)
        {
            case ForceFieldState.Inactive:
                {
                    fieldSprite.color = Color.white;

                    break;
                }
            case ForceFieldState.Generating:
                {
                    fieldSprite.color = Color.yellow;
                    speed = 0;

                    if (!isGenerating)
                    {
                        isGenerating = true;

                        Invoke("ChangeToActiveState", 2f);
                    }

                    break;
                }
            case ForceFieldState.Active:
                {
                    fieldSprite.color = Color.red;

                    if (!isGenerating)
                    {
                        isGenerating = true;

                        Invoke("ChangeToInactiveState", 5f);
                    }

                    break;
                }
            case ForceFieldState.Destroyed:
                {
                    fieldSprite.enabled = false;
                    generatorCollider.enabled = false;
                    fieldHealthBarUI.SetActive(false);

                    break;
                }
        }
    }

    // Backside main mechanic
    public void DamagingBackside(float playerDmg)
    {
        if (currentState == ForceFieldState.Inactive || currentState == ForceFieldState.Destroyed)
        {
            speed = stat.runningSpeed / 2;

            currentHP -= playerDmg;           

            barHealth.UpdateHealthBar(currentHP, stat.maxHP);

            StopCoroutine("CheckEnemyDeath");
            StartCoroutine("CheckEnemyDeath");

            if (!isGenerating)
            {
                isGenerating = true;

                Invoke("ChangeToGeneratingState", 1.5f);
            }
        }

        if (currentState == ForceFieldState.Generating)
        {

        }

        if (currentState == ForceFieldState.Active)
        {
            // Field Generator will damage back the player and push upward
            playerStat.PlayerTakeDamage(fieldStat.damage);
            playerMovement.PlayerRigid2d.AddForce(new Vector2(Mathf.Sign(player.transform.localScale.x) * -2000, 100), ForceMode2D.Impulse);
        }
    }

    // Only for damaging force field
    public void DamagingForceField(float playerDmg)
    {
        fieldHP -= playerDmg;

        fieldBarHealth.UpdateHealthBar(fieldHP, fieldStat.maxHP);

        if (fieldHP <= 0)
        {
            currentState = ForceFieldState.Destroyed;
            speed = stat.runningSpeed;
        }
    }

    // When player collides with Mower
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (player.transform.Find("UnderPlayerPosition").position.y >= rideHeight.position.y)
            {
                // Then ride it
                ridePos = player.transform.position.x - transform.position.x;
                isRiding = true;
            }
            // If player is super near Mower's head
            else if (Mathf.Abs(player.transform.position.x - frontDetection.position.x) <= 1.5f)
            {
                // Then attack player
                if (!isAttacking && currentState != ForceFieldState.Generating)
                {
                    isAttacking = true;
                    MowerAttack();
                }
            }         
        }
    }

    // When player get out of Mower's back
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {      
            // Stop riding
            isRiding = false;
        }
    }

    // Fix a bug when player auto ride after being attacked
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
        playerMovement.PlayerRigid2d.velocity = Vector2.zero;

        playerMovement.getUpCount = 0;
        playerMovement.isKnockDown = true;

        isAttacker = true;
        StartCoroutine("Attacking");

        if (dmgCoroutine != null)
        {
            StopCoroutine(dmgCoroutine);
        }

        dmgCoroutine = StartCoroutine(ApplyDamage(3, stat.damage));
    }

    protected override void KnockDownProcess()
    {
        base.KnockDownProcess();     // Still normal stun player

        if (!isAttacker) { return; }

        if (playerMovement.getUpCount == 10)
        {
            // Stop dealing damage and get back to original states
            if (dmgCoroutine != null)
            {
                StopCoroutine(dmgCoroutine);
            }

            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMovement.playerAttack.enabled = true;

            // Push player up 
            playerMovement.PlayerRigid2d.velocity = Vector2.up * 50;

            playerMovement.getUpCount = 0;
            playerMovement.isKnockDown = false;

            isAttacker = false;
            Invoke("ReturnPhysics", 0.5f);
        }        
    }

    void ReturnPhysics()
    {
        boxCollier.isTrigger = false;
        capsuleCollider.isTrigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        speed = stat.runningSpeed;
        isAttacking = false;
    }

    private IEnumerator Attacking()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollier.isTrigger = true;
        capsuleCollider.isTrigger = true;
        speed = stat.runningSpeed / 1.3f;

        yield return new WaitForSeconds(5f);

        ReturnPhysics();      
    }

    // Deal damage
    private IEnumerator ApplyDamage(int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while (currentCount < damageCount)
        {
            playerStat.PlayerTakeDamage(damageAmount);
            yield return new WaitForSeconds(1.5f);
            currentCount++;
        }
    }

    // Take no damage
    public override void TakeDamage(float playerDamage)
    {
        print("Not dealing dmg");
    }

    private void ChangeToGeneratingState()
    {
        if (currentState != ForceFieldState.Destroyed)
        {
            currentState = ForceFieldState.Generating;

            isGenerating = false;
        }
    }

    private void ChangeToActiveState()
    {
        if (currentState != ForceFieldState.Destroyed)
        {
            currentState = ForceFieldState.Active;
            speed = stat.runningSpeed;

            isGenerating = false;
        }
    }

    private void ChangeToInactiveState()
    {
        if (currentState != ForceFieldState.Destroyed)
        {
            currentState = ForceFieldState.Inactive;

            isGenerating = false;
        }
    }

    protected override void Movement()
    {
        base.Movement();

        if (!groundInfo || wallInfo)
        {
            fieldBarHealth.ScaleLeftUI(rb);

            if (isRiding)
            {
                isRiding = false;
                playerMovement.PlayerRigid2d.velocity = Vector2.up * 30;
            }
        }
    }
}

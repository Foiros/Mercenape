using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerBehaviour : EnemyStat
{
    [SerializeField] private EnemyStats fieldStat;
    private float fieldHP;
    private SpriteRenderer fieldSprite;

    private enum ForceFieldState { Inactive, Generating, Active, Destroyed }
    private ForceFieldState currentState;

    private GameObject fieldHealthBarUI;
    private EnemyHealthBar fieldBarHealth;

    private Coroutine dmgCoroutine;

    private bool isAttacking = false;
    private bool isRiding = false;
    private bool isGenerating = false;

    private float ridePos;

    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D generatorCollider;

    protected override void Start()
    {
        base.Start();   // Start both EnemyStat and MowerBehaviour   

        currentState = ForceFieldState.Inactive;

        fieldHealthBarUI = transform.GetChild(2).GetChild(0).gameObject;
        fieldBarHealth = fieldHealthBarUI.GetComponent<EnemyHealthBar>();

        fieldHP = fieldStat.maxHP;
        fieldBarHealth.UpdateHealthBar(fieldHP, fieldStat.maxHP);

        fieldSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        generatorCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        StunningProcess();

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

    public void DamagingBackside(float playerDmg)
    {
        if (currentState == ForceFieldState.Inactive || currentState == ForceFieldState.Destroyed)
        {
            speed = stat.runningSpeed / 2;

            currentHP -= playerDmg;           

            barHealth.UpdateHealthBar(currentHP, stat.maxHP);           

            CheckEnemyDeath();

            if (!isGenerating)
            {
                isGenerating = true;

                Invoke("ChangeToGeneratingState", 2f);
            }
        }

        if (currentState == ForceFieldState.Generating)
        {

        }

        if (currentState == ForceFieldState.Active)
        {
            // Field Generator will damage back the player and push upward
            playerStat.PlayerTakeDamage(fieldStat.damage);
            playerRigid.AddForce(new Vector2(Mathf.Sign(player.transform.localScale.x) * -2000, 100), ForceMode2D.Impulse);
        }
    }

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
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // If player's feet is higher than Mower's head
            if (player.transform.GetChild(1).name == "UnderPlayerPosition")
            {
                if (player.transform.GetChild(1).position.y > frontDetection.position.y)
                {
                    // Then ride it
                    ridePos = player.transform.position.x - transform.position.x;
                    isRiding = true;

                }
                // If player is super near Mower's head
                else if (Mathf.Abs(player.transform.position.x - frontDetection.position.x) <= .5f)
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
    }

    // When player stop riding
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {          
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
        playerRigid.velocity = Vector2.zero;

        escapingStunCount = 0;
        isStunning = true;

        StartCoroutine("Attacking");
        if (dmgCoroutine != null)
        {
            StopCoroutine(dmgCoroutine);
        }

        dmgCoroutine = StartCoroutine(ApplyDamage(3, stat.damage));
    }

    protected override void StunningProcess()
    {
        base.StunningProcess();     // Still normal stun player

        if (escapingStunCount == 8)
        {
            // Stop dealing damage and get back to original states
            if (dmgCoroutine != null)
            {
                StopCoroutine(dmgCoroutine);
            }

            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMovement.enabled = true;
            player.GetComponent<PlayerAttackTrigger>().enabled = true;
            player.GetComponent<Animator>().enabled = true;

            playerRigid.AddForce(new Vector2(0, 300), ForceMode2D.Impulse);

            escapingStunCount = 0;
            readyToSetStun = true;
            isStunning = false;

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

    IEnumerator Attacking()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollier.isTrigger = true;
        capsuleCollider.isTrigger = true;
        speed = 1.4f;

        yield return new WaitForSeconds(3f);

        ReturnPhysics();      
    }

    // Deal damage
    IEnumerator ApplyDamage(int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while (currentCount < damageCount)
        {
            playerStat.PlayerTakeDamage(damageAmount);
            yield return new WaitForSeconds(1f);
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
    }
}

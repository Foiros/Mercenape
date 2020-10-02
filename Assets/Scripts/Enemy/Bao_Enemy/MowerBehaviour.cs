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

        fieldStat.healthBarUI = transform.GetChild(2).GetChild(0).gameObject;
        fieldStat.sliderHealth = fieldStat.healthBarUI.transform.GetChild(0).gameObject;

        fieldHP = fieldStat.maxHP;
        fieldStat.UpdateHealthBar(fieldHP);

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
                    fieldStat.healthBarUI.SetActive(false);

                    break;
                }
        }
        Debug.DrawRay(player.transform.position, Vector2.right * 0.5f, Color.red);
    }

    // When player collides with Mower
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            /*ContactPoint2D[] contacts = new ContactPoint2D[1];
            int numContacts = col.GetContacts(contacts);

            for (int i = 0; i < numContacts; i++)
            {
                // Attack when hit the player in the front side
                if (Vector2.Distance(contacts[i].point, frontDetection.position) < 0.5f)
                {                  
                    if (!isAttacking)
                    {
                        isAttacking = true;

                        MowerAttack();
                    }
                }         
                // Player rides Mower
                else
                {
                    ridePos = player.transform.position.x - transform.position.x;
                    isRiding = true;
                }*/
            // If player's feet is higher than Mower's head
            if(player.transform.GetChild(1).position.y > frontDetection.position.y)
            {
                // Then ride it
                ridePos = player.transform.position.x - transform.position.x;
                isRiding = true;
            }
            else if (Mathf.Abs(player.transform.position.x - frontDetection.position.x) <= .5f)
            {
                if (!isAttacking)
                {
                    isAttacking = true;

                    MowerAttack();
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

            playerRigid.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * 3000, 0));

            escapingStunCount = 0;
            readyToSetStun = true;
            isStunning = false;

            Invoke("ReturnPhysics", 0.2f);
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

    public void DamagingBackside(float playerDmg)
    {
        if (currentState == ForceFieldState.Inactive || currentState == ForceFieldState.Destroyed)
        {
            currentHP -= playerDmg;
            speed = stat.runningSpeed / 2;

            stat.UpdateHealthBar(currentHP);
            StartCoroutine("HealthBarAnimation");

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
        print("circleeeee");
        fieldHP -= playerDmg;

        fieldStat.UpdateHealthBar(fieldHP);

        if (fieldHP <= 0)
        {
            currentState = ForceFieldState.Destroyed;
        }
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
        currentState = ForceFieldState.Active;
        speed = stat.runningSpeed;

        isGenerating = false;
    }

    private void ChangeToInactiveState()
    {
        currentState = ForceFieldState.Inactive;

        isGenerating = false;
    }

    protected override void Movement()
    {
        base.Movement();       
    }
}

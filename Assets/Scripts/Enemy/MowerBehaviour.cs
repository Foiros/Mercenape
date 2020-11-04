using System;
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

    private bool isAttacking;
    private bool isRiding = false;
    private bool isGenerating;

    private bool isBackSideHit;
    private bool isGeneratorHit;

    [SerializeField] private Transform backside;
    private float ridePos;
    [SerializeField] private Transform rideHeight;

    private CapsuleCollider capsuleCollider;
    private SphereCollider generatorCollider;

    private Animator animatorMower;

    protected override void Awake()
    {
        base.Awake();

        fieldHealthBarUI = backside.GetChild(0).gameObject;
        fieldBarHealth = fieldHealthBarUI.GetComponent<EnemyHealthBar>();

        fieldSprite = backside.GetComponent<SpriteRenderer>();

        capsuleCollider = backside.GetComponent<CapsuleCollider>();
        generatorCollider = backside.GetComponent<SphereCollider>();

        animatorMower = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        currentState = ForceFieldState.Inactive;
        Inactive();

        isAttacking = false;
        isGenerating = false;

        fieldHP = fieldStat.maxHP;
        fieldBarHealth.UpdateHealthBar(fieldHP, fieldStat.maxHP);

        capsuleCollider.enabled = true;

        animatorMower.SetBool("IsDestroyed", false); ;

        fieldSprite.enabled = true;
        generatorCollider.enabled = true;
        fieldHealthBarUI.SetActive(true);
    }

    private void Update()
    {
        // Check if player is knocked down by Mower
        KnockDownProcess();
    }

    #region Generator State
    private void Generating()
    {
        if (currentState != ForceFieldState.Destroyed)
        {
            currentState = ForceFieldState.Generating;
            animatorMower.SetTrigger("Generating");

            fieldSprite.color = Color.yellow;
            speed = 0;
            isGenerating = false;

            Invoke("Active", 2f);
        }
    }

    private void Active()
    {
        if (currentState != ForceFieldState.Destroyed)
        {
            currentState = ForceFieldState.Active;
            animatorMower.SetBool("IsActive", true);

            fieldSprite.color = Color.red;
            speed = stat.runningSpeed;

            Invoke("Inactive", 5f);
        }
    }

    private void Inactive()
    {
        if (currentState != ForceFieldState.Destroyed)
        {
            currentState = ForceFieldState.Inactive;
            animatorMower.SetBool("IsActive", false);

            fieldSprite.color = Color.white;
        }
    }

    private void Destroyed()
    {
        currentState = ForceFieldState.Destroyed;       
        animatorMower.SetBool("IsDestroyed", true);

        speed = stat.runningSpeed;
        fieldSprite.enabled = false;
        generatorCollider.enabled = false;
        fieldHealthBarUI.SetActive(false);
    }
    #endregion


    #region Damaging Mower and override bleed
    // Check whether backside or generator get hit, then act accordingly
    protected override void EnemyGetHit(bool isMowerBackSide, bool isMowerGenerator, Collider selfCol, float playerDmg)
    {
        this.isBackSideHit = isMowerBackSide;
        this.isGeneratorHit = isMowerGenerator;

        base.EnemyGetHit(isMowerBackSide, isMowerGenerator, selfCol, playerDmg);
    }

    // Special TakerDamage mechanic for Mower
    public override void TakeDamage(float playerDamage)
    {
        if (isBackSideHit)
        {
            DamagingBackside(() => { base.TakeDamage(playerDamage); });
        }

        if (isGeneratorHit) { DamagingForceField(playerDamage); }

        // And take no damage if none of them are true
    }   
    
    // Backside main mechanic
    public void DamagingBackside(Action TakeDamage)
    {      
        if (currentState == ForceFieldState.Inactive || currentState == ForceFieldState.Destroyed)
        {           
            TakeDamage();

            if (currentState == ForceFieldState.Inactive) { speed = stat.runningSpeed / 2; }
            
            if (currentHP <= 0)
            {
                capsuleCollider.enabled = false;

                animatorMower.SetTrigger("Death");
            }

            if (!isGenerating)
            {
                isGenerating = true;

                Invoke("Generating", 1.5f);
            }
        }

        if (currentState == ForceFieldState.Generating)
        {

        }

        if (currentState == ForceFieldState.Active)
        {
            // Field Generator will damage back the player and push upward
            playerHealth.PlayerTakeDamage(fieldStat.damage);
            playerMovement.PlayerRigid2d.AddForce(new Vector3(Mathf.Sign(player.transform.localScale.x) * -2000, 100), ForceMode.Impulse);
        }
    }

    // Only for damaging force field
    public void DamagingForceField(float playerDmg)
    {
        fieldHP -= playerDmg;
        fieldBarHealth.UpdateHealthBar(fieldHP, fieldStat.maxHP);

        DamagePopUp.Create(PopUpPos(backside), playerDmg);

        if (fieldHP <= 0)
        {
            Destroyed();
        }
    }

    public override void ApplyBleeding(float damage, float duration, int ticks, Collider selfCol)
    {
        // Don't bleed the generator
        if (isGeneratorHit) { return; }

        // Don't bleed Mower's front side
        if (!isBackSideHit) { return; }

        // Only bleed when Inactive or Destroyed
        if (currentState == ForceFieldState.Inactive || currentState == ForceFieldState.Destroyed)
        {
            base.ApplyBleeding(damage, duration, ticks, selfCol);
        }
    }
    #endregion

    // When player collides with Mower
    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // If player is super near Mower's head
            if (Mathf.Abs(player.transform.position.x - frontDetection.position.x) <= 1.5f)
            {
                // Then attack player
                if (!isAttacking && currentState != ForceFieldState.Generating)
                {
                    playerMovement.isPlayerBlock = false;   // Cancel block if player is
                  
                    MowerAttack();
                }
            }
        }
    }

    #region Mower attack
    private void MowerAttack()
    {
        isAttacking = true;
        playerMovement.PlayerRigid2d.velocity = Vector3.zero;       
        
        KnockPlayerDown();

        if (dmgCoroutine != null)
        {
            StopCoroutine(dmgCoroutine);           
        }
        StartCoroutine("Attacking");
    }

    private IEnumerator Attacking()
    {
        rb.useGravity = false;
        boxCollier.isTrigger = true;
        capsuleCollider.isTrigger = true;
        speed = stat.runningSpeed / 1.8f;

        dmgCoroutine = StartCoroutine(ApplyDamage(5, stat.damage));

        yield return new WaitForSeconds(6f);

        ReturnPhysics();
    }

    // Deal damage
    private IEnumerator ApplyDamage(int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while (currentCount < damageCount)
        {
            playerHealth.PlayerTakeDamage(damageAmount);
            yield return new WaitForSeconds(1.2f);
            currentCount++;
        }
    }
    #endregion

    protected override void KnockDownProcess()
    {
        base.KnockDownProcess();     // Still normal stun player      
    }

    protected override void PlayerUp()
    {
        // Stop dealing damage
        StopCoroutine("Attacking");
        if (dmgCoroutine != null) { StopCoroutine(dmgCoroutine); }
        
        // If not Mower, return 
        if (enemyID != GetInstanceID()) { return; }
        
        // Push player up 
        playerMovement.PlayerRigid2d.velocity = Vector3.up * 20;

        Invoke("ReturnPhysics", 0.5f);

        base.PlayerUp();
    }

    void ReturnPhysics()
    {
        boxCollier.isTrigger = false;
        capsuleCollider.isTrigger = false;
        rb.useGravity = true;
        speed = stat.runningSpeed;
        isAttacking = false;
    }

    protected override void Movement()
    {
        base.Movement();

        animatorMower.SetFloat("MovementSpeed", speed / stat.runningSpeed);

        // Also turn healthbar of the generator
        if (!groundInfo || wallInfo)
        {
            fieldBarHealth.ScaleRightUI(rb);

            if (isRiding)
            {
                isRiding = false;
                playerMovement.PlayerRigid2d.velocity = Vector3.up * 30;
            }
        }
    }

    protected override Vector3 PopUpPos(Transform trans)
    {
        var pos = new Vector3(trans.position.x + UnityEngine.Random.Range(-4f, 4f), trans.position.y + 8, -9);

        return pos;
    }
}

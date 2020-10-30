﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Bao: Shred's Behaviour, child of EnemyBehaviour
// Edited by Arttu Paldán on 22-23.10.2020: Made some changes to the knockdown effect to implement knocked down animations. Also implemented the weapon bleed damage. 
public class ShredBehaviour : EnemyBehaviour
{      
    [SerializeField] private float bleedChance;   

    private Coroutine co;

    private Animator animatorShred;

    private bool isStaggering = false;
    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();   // Start both EnemyBehaviour and ShredBehaviour  

        animatorShred = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if player is knocked down by Shred
        KnockDownProcess();

        ShredCheck();
    }

    private void ShredCheck()
    {
        // Don't check if dead or is staggering
        if (currentHP <= 0 || isStaggering) { return; }
        
        // If player is not in front of Shred's peak, don't attack
        if(!Physics.Raycast(frontDetection.position, transform.right, 2.5f, LayerMask.GetMask("Player"))) { return; }

        // If player face against Shred and is blocking
        if (IsFacingRight() != playerMovement.FaceRight && playerMovement.isPlayerBlock)
        {
            playerMovement.animator.SetTrigger("TakingHitBlocking");
            // Block and immobilize Shred
            StopCoroutine("StaggerShred");
            StartCoroutine("StaggerShred");
        }
        else
        {
            playerMovement.isPlayerBlock = false;
            ShredAttack();
        }
    }

    // When Shred attacks and applies damage
    private void ShredAttack()
    {       
        // Only attack once
        if (isAttacking) { return; }

        KnockPlayerDown();

        StartCoroutine("Attacking");
        playerHealth.PlayerTakeDamage(stat.damage);

        // Check bleed chance of Shred, then apply 
        if (Random.Range(0f, 100f) < bleedChance)
        {
            if (co != null)
            {
                StopCoroutine(co);
            }
            co = StartCoroutine(ApplyBleedDamage(1, 3, 2));
        }
    }

    // Make sure Shred pass through player after attaking
    IEnumerator Attacking()
    {
        // Pass through player
        rb.useGravity = false;
        boxCollier.isTrigger = true;
        isAttacking = true;
        
        yield return new WaitForSeconds(0.7f);

        // Return to original states
        boxCollier.isTrigger = false;
        rb.useGravity = true;
        isAttacking = false;
    }

    // Do bleed damage (per sec for now)
    IEnumerator ApplyBleedDamage(float damageDuration, int damageCount, int damageAmount)
    {
        int currentCount = 1;
    
        while(currentCount <= damageCount)
        {
            playerHealth.PlayerHP -= damageAmount;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }       
    }
   
    IEnumerator StaggerShred()
    {
        isStaggering = true;
        animatorShred.SetBool("Staggering", isStaggering);

        speed = -stat.runningSpeed / 1.5f;

        yield return new WaitForSeconds(0.15f);

        rb.isKinematic = true;
        speed = 0;

        yield return new WaitForSeconds(2f);

        isStaggering = false;
        animatorShred.SetBool("Staggering", isStaggering);

        yield return new WaitForSeconds(0.15f);

        rb.isKinematic = false;
        speed = stat.runningSpeed;
    }

    protected override void KnockDownProcess()
    {
        base.KnockDownProcess();     // Still normally stun player
        
        if (!isAttacker) { return; }

        playerHealth.SetNeededSpace(stat.spaceToGetUp);

        if (playerMovement.getUpCount >= stat.spaceToGetUp)
        {
            playerMovement.PlayerBounceUp();

            isAttacker = false;
        }
    }

    public override void TakeDamage(float playerDamage)
    {
        base.TakeDamage(playerDamage);

        animatorShred.Play("Armature|StaggeredTakeHit", 0, 0f);

        // If Shred is dead
        if (currentHP <= 0)
        {
            animatorShred.SetTrigger("Death");
        }
    }
}
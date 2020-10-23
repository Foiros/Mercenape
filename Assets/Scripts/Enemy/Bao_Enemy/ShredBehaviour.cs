﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Bao: Shred's Behaviour, child of EnemyBehaviour
// Edited by Arttu Paldán on 22-23.10.2020: Made some changes to the knockdown effect to implement knocked down animations. Also implemented the weapon bleed damage. 
public class ShredBehaviour : EnemyBehaviour
{      
    [SerializeField] private float bleedChance;   

    private Coroutine co;

    protected override void Start()
    {
        base.Start();   // Start both EnemyBehaviour and ShredBehaviour       
    }

    private void Update()
    {
        // Check if player is knocked down by Shred
        KnockDownProcess();
    }

    // Hit player
    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {          
            // If player face against Shred and is blocking
            if (IsFacingRight() != playerMovement.FaceRight && playerMovement.isPlayerBlock)
            {
                playerMovement.animator.SetBool("TakingHitBlocking", true);
                // Block and immobilize Shred
                StopCoroutine("ImmobilizeShred");
                StartCoroutine("ImmobilizeShred");
            }
            else
            {
                // ShredAttack();
            }
        }
    }

    // When Shred attacks and applies damage
    private void ShredAttack()
    {
        // If player is not in front of Shred's peak, don't attack
        if (Mathf.Abs(player.transform.position.x - frontDetection.position.x) > 3f) { return; }

        isAttacker = true;
        StartCoroutine("Attacking");
        playerStat.PlayerTakeDamage(stat.damage);
        playerMovement.getUpCount = 0;
        playerMovement.isBeingKnockedDown = true;

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
        
        yield return new WaitForSeconds(0.7f);

        // Return to original states
        boxCollier.isTrigger = false;
        rb.useGravity = true;
    }

    // Do bleed damage (per sec for now)
    IEnumerator ApplyBleedDamage(float damageDuration, int damageCount, int damageAmount)
    {
        int currentCount = 0;

        while(currentCount < damageCount)
        {
            playerStat.PlayerHP -= damageAmount;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }       
    }
   
    IEnumerator ImmobilizeShred()
    {
        //speed = -stat.runningSpeed / 20;
        speed = 0;
        rb.AddForce(transform.right * -10, ForceMode.VelocityChange);

        yield return new WaitForSeconds(1f);

        speed = stat.runningSpeed;
    }

    protected override void KnockDownProcess()
    {
        base.KnockDownProcess();     // Still normally stun player
        
        if (!isAttacker) { return; }
    }

    public override void TakeDamage(float playerDamage)
    {
        base.TakeDamage(playerDamage);
    }

    public override void ApplyBleeding(float damage, float duration, int ticks)
    {
        base.ApplyBleeding(damage, duration, ticks);
        StartCoroutine(BleedTick());
    }

    IEnumerator BleedTick()
    {
        while (currentBleedTicks <= bleedTicks)
        {
            TakeDamage(weaponBleedDamage);
            yield return new WaitForSeconds(weaponBleedDuration);
            currentBleedTicks++;
        }
    }
}
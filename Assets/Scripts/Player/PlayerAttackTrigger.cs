﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Arttu Paldán modified on 24.9.2020: Added SetWeaponStats function for setting 
// Edited by Arttu Paldán on 22-23.10.2020: Added the ability to draw the attack radius with Vector3's. Also added the ability for player attack to cause bleed effect. 
public class PlayerAttackTrigger : MonoBehaviour
{
    private WeaponStates weaponStates;
    private PlayerMovement playerMovement;
    
    private List<AbstractWeapon> weapons;

    public float TimeDelayAttack; // to check if there still countdown time untill player can atk again
    public float PlayerDelayAttackTime; // can exchange to weapon atk rate later

    public Transform Attackpos;
    private Vector3 scaleChange, positionChange;

    public LayerMask EnemyLayerMask;
    [SerializeField] private float PlayerDamage;
    [SerializeField] private Transform edgePos;

    public Transform frontPlayerPosition;
    private int weaponID;
    [SerializeField] private float weaponSpeed, weaponBleedDamage, weaponBleedDuration;
    [SerializeField] private int bleedTicks;

    public Animator PlayerAnimator;
    public AnimationClip attackAnim;
    
    private bool IsPlayerAttack = false;
    
    public event Action<bool, bool, Collider, float> OnHitEnemy;  // 1st bool is Mower backside, 2nd bool is generator
    public event Action<float, float, int, Collider> OnBleedEnemy;  // weaponBleedDamage, weaponBleedDuration, bleedTicks

    void Awake()
    {
        weaponStates = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WeaponStates>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
        IsPlayerAttack = false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {      
        SetWeaponStats();
    }

    void FixedUpdate()
    {      
        if (!playerMovement.isPlayerBlock)
        { PlayerAttack(); }       
    }

    void SetWeaponStats()
    {
        weaponID = weaponStates.GetChosenWeaponID();
        PlayerDamage = weaponStates.GetWeaponImpactDamage();
        weaponSpeed = weaponStates.GetWeaponSpeed();
        PlayerAnimator.SetFloat("AttackSpeed", weaponSpeed);
        
        weaponBleedDamage = weaponStates.GetWeaponBleedDamage();
        weaponBleedDuration = weaponStates.GetBleedDuration();
        bleedTicks = weaponStates.GetWeaponBleedTicks();

        positionChange = weapons[weaponID].GetHitBoxLocation();
        scaleChange = weapons[weaponID].GetHitBoxSize();

        Attackpos.localPosition = positionChange;
        Attackpos.localScale = scaleChange;
    }

    bool CheckMouseInput() { return Input.GetKey(KeyCode.Mouse0); }

    public void PlayerAttack()
    {
        // Cannot attack if player is getting up
        if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("BounceBack")) { return; }       
        
        if (CheckMouseInput() && !IsPlayerAttack )
        {
            IsPlayerAttack = true;
            PlayerAnimator.SetTrigger("Attack");

            // (Holding mouse) When attack anim finish, it continues the next one, not standing still as before
            // weaponSpeed = 1 is normal, = 2 is fast double, etc. (could be less than 1 for slower speed)
            TimeDelayAttack = DelayTime();        
        }
        else
        {
            CheckAttackStatus();
        }      
    }

    // When actually hit enemy with the weapon
    public void HitEnemy()
    {
        Collider[] enemiesToDamage = Physics.OverlapBox(Attackpos.position, Attackpos.localScale, Quaternion.identity, EnemyLayerMask);
        //Collider[] enemiesToDamage = new Collider[10];
        //int numHit = Physics.OverlapSphereNonAlloc(edgePos.position, 0.5f, enemiesToDamage, EnemyLayerMask);
        
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {          
            // Created  and edited by Bao: Detect enemy and attack
            if (enemiesToDamage[i].GetType() == typeof(CapsuleCollider)) // Mower's back
            {
                // Hit backside but not generator
                OnHitEnemy?.Invoke(true, false, enemiesToDamage[i], PlayerDamage);
            }
            else if (enemiesToDamage[i].GetType() == typeof(SphereCollider)) // Mower's generator
            {
                // Hit generator but not backside
                OnHitEnemy?.Invoke(false, true, enemiesToDamage[i], PlayerDamage);
            }
            else  // Not Mower, meaning Shred
            {
                // None of generator nor backside
                OnHitEnemy?.Invoke(false, false, enemiesToDamage[i], PlayerDamage);
            }

            // Then bleed enemy (Mower script make sure the generator don't bleed)
            if (weaponBleedDamage > 0 && bleedTicks > 0)
            {
                OnBleedEnemy?.Invoke(weaponBleedDamage, weaponBleedDuration, bleedTicks, enemiesToDamage[i]);
            }
        }
    }  

    void CheckAttackStatus()
    {
        if (TimeDelayAttack <= 0)
        {
            IsPlayerAttack = false;
        }
        else if (IsPlayerAttack)
        {
            TimeDelayAttack -= Time.deltaTime;
        }      
    }

    float GetAttackAnimLength() { return attackAnim.length; }

    public float DelayTime()
    {
        return (1 / weaponSpeed) * GetAttackAnimLength();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Attackpos.position, Attackpos.localScale);
        //Gizmos.DrawSphere(edgePos.position, 0.5f);
    }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Arttu Paldán modified on 24.9.2020: Added SetWeaponStats function for setting 
// Edited by Arttu Paldán on 22-23.10.2020: Added the ability to draw the attack radius with Vector3's. Also added the ability for player attack to cause bleed effect. 
public class PlayerAttackTrigger : MonoBehaviour
{
    private WeaponStates weaponStates;
    private PlayerMovement playerMovement;

    private List<AbstractWeapon> weapons;

    [SerializeField] private float TimeDelayAttack; // to check if there still countdown time untill player can atk again
    public float PlayerDelayAttackTime; // can exchange to weapon atk rate later

    public Transform Attackpos;
    private Vector3 scaleChange, positionChange;

    public LayerMask EnemyLayerMask;
    [SerializeField] private float PlayerDamage;

    public Transform frontPlayerPosition;
    private int weaponID;
    [SerializeField] private float weaponSpeed, weaponBleedDamage, weaponBleedDuration;
    private int bleedTicks;

    public Animator PlayerAnimator;
    
    private bool IsPlayerAttack = false;

    void Awake()
    {
        weaponStates= GameObject.FindGameObjectWithTag("GameManager").GetComponent<WeaponStates>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = gameObject.GetComponent<Animator>();
        IsPlayerAttack = false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        SetWeaponStats();
    }


    // Update is called once per frame
    void Update()
    {
        CheckMouseInput();  
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
        if (CheckMouseInput() && !IsPlayerAttack)
        {
            IsPlayerAttack = true;
            SetAnimationState();

            TimeDelayAttack = PlayerDelayAttackTime;

            Collider[] enemiesToDamage = Physics.OverlapBox(Attackpos.position, Attackpos.localScale, Quaternion.identity, EnemyLayerMask);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                // Create by Bao: Attacking Mower
                if (enemiesToDamage[i].GetType() == typeof(CapsuleCollider))
                {
                    enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().DamagingBackside(PlayerDamage);

                    if (weaponBleedDamage > 0 && bleedTicks > 0)
                    {
                        enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().ApplyBleeding(weaponBleedDamage, weaponBleedDuration, bleedTicks);
                    }
                }
                else if (enemiesToDamage[i].GetType() == typeof(SphereCollider))
                {
                    enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().DamagingForceField(PlayerDamage);
                }
                else
                {
                    enemiesToDamage[i].GetComponentInParent<EnemyBehaviour>().TakeDamage(PlayerDamage);

                    if (weaponBleedDamage > 0 && bleedTicks > 0)
                    {
                        enemiesToDamage[i].GetComponentInParent<EnemyBehaviour>().ApplyBleeding(weaponBleedDamage, weaponBleedDuration, bleedTicks);
                    }
                }
            }
        }
        else
        {
            CheckAttackStatus();
        }
    }

    void CheckAttackStatus()
    {
        if (TimeDelayAttack <= 0)
        {
            IsPlayerAttack = false;

            SetAnimationState();
        }
        else if (IsPlayerAttack)
        {
            TimeDelayAttack -= Time.deltaTime;
        }
    }

    void SetAnimationState()
    {
        if (IsPlayerAttack)
        {
            PlayerAnimator.speed = weaponSpeed;
            PlayerAnimator.SetBool("IsAttacking", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsAttacking", false);
            PlayerAnimator.speed = 1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Attackpos.position, Attackpos.localScale);
    }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Arttu Paldán modified on 24.9.2020:
public class PlayerAttackTrigger : MonoBehaviour
{
    private WeaponStates weaponStates;

    private List<AbstractWeapon> weapons;
    
    private float TimeDelayAttack; // to check if there still countdown time untill player can atk again
    public float PlayerDelayAttackTime; // can exchange to weapon atk rate later

    public Transform Attackpos;
    public float AttackRange;
    public LayerMask EnemyLayerMask;
    public float PlayerDamage;

    public Transform frontPlayerPosition;
    private int weaponID;
    private float weaponSpeed;
    // private float hitboxDistFromPlayer;

    public Animator PlayerAnimator;
    
    private bool IsPlayerAttack = false;

    PlayerMovement playerMovement;

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
        AttackRange = weapons[weaponID].GetHitBox();
        PlayerDamage = weaponStates.GetWeaponImpactDamage();
        weaponSpeed = weaponStates.GetWeaponSpeed();
    }

    bool CheckMouseInput()    
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    public void PlayerAttack()
    {
        if(CheckMouseInput()&& !IsPlayerAttack)
        {
            PlayerAnimator.speed = weaponSpeed;

            IsPlayerAttack = true;
            TimeDelayAttack = PlayerDelayAttackTime;

            PlayerAnimator.SetBool("IsAttacking", true);

            Collider[] enemiesToDamage = Physics.OverlapBox(Attackpos.position, transform.localScale * AttackRange, Quaternion.identity, EnemyLayerMask);
          
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {             
                // Create by Bao: Attacking Mower
                if (enemiesToDamage[i].GetType() == typeof(CapsuleCollider))
                {
                    enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().DamagingBackside(PlayerDamage);
                    PlayerAnimator.speed = 1;
                }
                else if (enemiesToDamage[i].GetType() == typeof(SphereCollider))
                {
                    enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().DamagingForceField(PlayerDamage);
                    PlayerAnimator.speed = 1;
                }
                else
                {
                    enemiesToDamage[i].GetComponentInParent<EnemyBehaviour>().TakeDamage(PlayerDamage);
                    PlayerAnimator.speed = 1;
                }
            }
        }

        if (IsPlayerAttack)
        {
            if(TimeDelayAttack > 0)
            {
                TimeDelayAttack -= Time.deltaTime;
            }
            else
            {
                IsPlayerAttack = false;
                PlayerAnimator.SetBool("IsAttacking", false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Attackpos.position, transform.localScale * AttackRange);
    }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

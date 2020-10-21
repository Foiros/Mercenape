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
    public Animation anim;
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
        Debug.DrawRay(Attackpos.position, Vector2.right * AttackRange, Color.green);

        PlayerAnimator.SetBool("IsPlayerAttack", IsPlayerAttack);
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

        weaponSpeed = weaponStates.GetWeaponSpeed();
        // anim["PlayerAttack"].speed = weaponSpeed;

        PlayerDamage = weaponStates.GetWeaponImpactDamage();
    }

    bool CheckMouseInput()    
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    public void PlayerAttack()
    {
        if(CheckMouseInput()&& !IsPlayerAttack)
        {
            IsPlayerAttack = true;
            TimeDelayAttack = PlayerDelayAttackTime;

            Collider[] enemiesToDamage = Physics.OverlapBox(Attackpos.position, transform.localScale * AttackRange, Quaternion.identity, EnemyLayerMask);
          
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {             
                // Create by Bao: Attacking Mower
                if (enemiesToDamage[i].GetType() == typeof(BoxCollider))
                {
                    enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().DamagingBackside(PlayerDamage);
                }
                else if (enemiesToDamage[i].GetType() == typeof(BoxCollider))
                {
                    enemiesToDamage[i].GetComponentInParent<MowerBehaviour>().DamagingForceField(PlayerDamage);
                }
                else
                {
                    enemiesToDamage[i].GetComponentInParent<EnemyBehaviour>().TakeDamage(PlayerDamage);
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

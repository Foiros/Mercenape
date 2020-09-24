using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Arttu Paldán modified on 24.9.2020:
public class PlayerAttackTrigger : MonoBehaviour
{
    private SetActualWeapon setActualWeapon;
    private AssetManager assetManager;

    private AbstractWeapon[] weapons;
    
    private float TimeDelayAttack; // to check if there still countdown time untill player can atk again
    public float PlayerDelayAttackTime; // can exchange to weapon atk rate later

    public Transform Attackpos;
    public float AttackRange;
    public LayerMask EnemyLayerMask;
    public int PlayerDamage;

    public Transform frontPlayerPosition;
    private int weaponID;
    private int weaponSpeed;
    private float hitboxDistFromPlayer;

    public Animator PlayerAnimator;
    private bool IsPlayerAttack = false;

    PlayerMovement playerMovement;

    void Awake()
    {
        setActualWeapon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SetActualWeapon>();
        assetManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AssetManager>();

        SetUpWeaponsArray();
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
        PlayerAnimator.SetBool("IsPlayerAttack", IsPlayerAttack);
        CheckMouseInput();
    }
    void FixedUpdate()
    {
        if (!playerMovement.isPlayerBlock)
        { PlayerAttack(); }
    }
    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, 30, 0.3f, 1f, assetManager.weaponImages[0], null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, 20, 0.3f, 2f, assetManager.weaponImages[1], null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, 10, 0.3f, 1f, assetManager.weaponImages[2], null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, 20, 0.3f, 5f, assetManager.weaponImages[3], null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    void SetWeaponStats()
    {
        weaponID = setActualWeapon.GetChosenID();
        hitboxDistFromPlayer = weapons[weaponID].hitBoxLocation;

        frontPlayerPosition.position = new Vector3(Attackpos.position.x + hitboxDistFromPlayer, Attackpos.position.y, 0);
        AttackRange = weapons[weaponID].hitBox;

        weaponSpeed = setActualWeapon.GetWeaponSpeed();
        PlayerAnimator.speed = weaponSpeed;
        
        PlayerDamage = setActualWeapon.GetWeaponImpactDamage();
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
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(Attackpos.position, AttackRange, EnemyLayerMask);
          
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(PlayerDamage);
                Debug.Log("attacking" + enemiesToDamage[i] + PlayerDamage );
                Debug.Log(enemiesToDamage[i].GetComponent<EnemyStat>().currentHP);
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
        Gizmos.DrawSphere(Attackpos.position, AttackRange);
    }
}

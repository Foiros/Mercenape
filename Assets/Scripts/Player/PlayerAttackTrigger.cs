using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    private float TimeDelayAttack; // to check if there still countdown time untill player can atk again
    public float PlayerDelayAttackTime; // can exchange to weapon atk rate later

    public Transform Attackpos;
    public float AttackRange;
    public LayerMask EnemyLayerMask;
    public int PlayerDamage;

    public Animator PlayerAnimator;
    private bool IsPlayerAttack = false;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = gameObject.GetComponent<Animator>();
        IsPlayerAttack = false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
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



    
}

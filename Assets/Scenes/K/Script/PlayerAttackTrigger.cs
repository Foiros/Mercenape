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
    public Collider2D AttackHitBox;
    private bool IsPlayerAttack = false;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = gameObject.GetComponent<Animator>();
        IsPlayerAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
        PlayerAnimator.SetBool("IsPlayerAttack", IsPlayerAttack);
    }


    public void PlayerAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& !IsPlayerAttack)
        {
            IsPlayerAttack = true;
            TimeDelayAttack = PlayerDelayAttackTime;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(Attackpos.position, AttackRange, EnemyLayerMask);
            Debug.Log("attacking");
           

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(PlayerDamage);
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



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpos.position, AttackRange);

    }
}

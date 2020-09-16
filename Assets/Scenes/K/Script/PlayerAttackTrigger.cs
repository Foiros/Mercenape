using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public float AttackDelay = 0.3f;
    public bool IsPlayerAttack = false;
   
    public Animator PlayerAnimator;
    public Collider2D AttackHitBox;

    private void Awake()
    {
        AttackHitBox.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
        
    }


    public void PlayerAttack()
    {


        if (Input.GetKey(KeyCode.Space) && !IsPlayerAttack)
        {
            IsPlayerAttack = true;
            AttackHitBox.enabled = true;
            AttackDelay = 0.3f;
        }

        if (IsPlayerAttack == true)
        {
            if (AttackDelay > 0)
            {
                AttackDelay -= Time.deltaTime;
            }
            else
            {
                IsPlayerAttack = false;
                AttackHitBox.enabled = false;
            }

        }

        PlayerAnimator.SetBool("IsPlayerAttack", IsPlayerAttack);

    }
}

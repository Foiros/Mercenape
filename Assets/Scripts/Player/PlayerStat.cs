using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat playerStat;
    public PlayerCurrency playerCurrency;

    public GameObject player;
    public int PlayerHP;
    public int PlayerMaxHP;
    public int PlayerDamage;


    void Start()
    {
        PlayerHP = PlayerMaxHP;
        
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))    
        {
            PlayerHP -= 30;
        }


    }


    void CheckPlayerDeath()
    {
        
    }

    public void PlayerTakeDamage(int EnemyDamage)
    {
        PlayerHP -= EnemyDamage;
        CheckPlayerDeath();    //ineffective should only be called when get damage 
        
    }

    

}

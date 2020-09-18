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
    }
    void CheckPlayerDeath()
    {
        if (PlayerHP <= 0)
        {
            Destroy(gameObject);
            playerCurrency.PlayerKarma = 0;// could alway set hp and karma at the begining of a sence
            SceneManager.LoadScene("Thuyet_Test_Karma");


        }
    }

    public void PlayerTakeDamage(int EnemyDamage)
    {
        PlayerHP -= EnemyDamage;
        CheckPlayerDeath();    //ineffective should only be called when get damage 
        
    }

    

}

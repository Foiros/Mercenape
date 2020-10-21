﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public GameObject player;
 
   
    public int PlayerHP;
    public int PlayerMaxHP;
    public int PlayerDamage;

    PlayerCurrency playerCurrency;
    playerUI playerUI;

    public int lostGold;
        public int lostKarma;

    void Start()
    {
        playerUI = transform.Find("PlayerUI").GetComponent<playerUI>();
        playerCurrency = transform.GetComponent<PlayerCurrency>();

        PlayerHP = PlayerMaxHP;
        
        
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Y))
        {
            updateHealth(10);
           
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            updateHealth(-10);
        }

        
    }

    //Ossi's take on player HP change
    public void updateHealth(int changeHP)
    {
        PlayerHP += changeHP;
        if(PlayerHP > PlayerMaxHP)
        {
            //This is make sure that the Healthbar component isn't unnecessarily called.
            PlayerHP = PlayerMaxHP;
        } 
        else
        {
        if(PlayerHP <= 0)
            {
                // This checks if the player takes fatal damage.
                PlayerHP = 0;
            }
            //Add the new health values to change the fill amount of the healthbar.
            //  healthBar.updateHealthBar(PlayerMaxHP, PlayerHP);
            playerUI.SetCurrentHP(PlayerHP);
            CheckPlayerDeath();

        }

            
    }


    void CheckPlayerDeath()
    {
        if (PlayerHP <= 0)
        {
            // This checks if the player takes fatal damage.
            PlayerHP = 0;

            UpdateCurrency();
          
            //Debug.Log("Dead.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateCurrency()
    {
        lostGold = playerCurrency.playerGold * 10 / 100;
        lostKarma = playerCurrency.playerKarma * 10 / 100;

        playerCurrency.playerGold -= lostGold;

        if (playerCurrency.playerGold < 0)
        {
            playerCurrency.playerGold = 0;
        }

        playerCurrency.playerKarma -= lostKarma;
        
        if (playerCurrency.playerKarma < 0)
        {
            playerCurrency.playerKarma = 0;
        }
        playerUI.MoneyText();
        playerUI.KarmaText();
        playerUI.SetKarmaValue();
     

        SaveManager.SaveCurrency(playerCurrency);
        

    }

   void StartDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    // This can be handled by the UpdateHealth().
    public void PlayerTakeDamage(int EnemyDamage)
    {
        PlayerHP -= EnemyDamage;
        playerUI.SetCurrentHP(PlayerHP);
        CheckPlayerDeath();    
        
    }

  

}

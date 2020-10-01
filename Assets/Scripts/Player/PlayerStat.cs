using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
   
    
    public GameObject player;
    public Healthbar_Ossi healthBar;
   
    public int PlayerHP;
    public int PlayerMaxHP;
    public int PlayerDamage;

    PlayerHealthBar playerHealthBar;


    void Start()
    {
        /*healthBar = GetComponentInChildren<Healthbar_Ossi>();
        if(healthBar != null)
        {
            Debug.Log("PlayerHP bar found.");
        }*/

        playerHealthBar = transform.Find("PlayerUI").GetComponent<PlayerHealthBar>();

        PlayerHP = PlayerMaxHP;
        
    }
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.X))    
        //{
        //    PlayerHP -= 30;
        //}

        //Ossi's version:
        if(Input.GetKeyDown(KeyCode.Y))
        {
            updateHealth(10);
           
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            updateHealth(-10);
        }

        CheckPlayerDeath();
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
                Debug.Log("Dead.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            //Add the new health values to change the fill amount of the healthbar.
            //  healthBar.updateHealthBar(PlayerMaxHP, PlayerHP);
            playerHealthBar.SetCurrentHP(PlayerHP);
        }    
        
        
    }


    void CheckPlayerDeath()
    {
        if (PlayerHP <= 0)
        {
            // This checks if the player takes fatal damage.
            PlayerHP = 0;
            Debug.Log("Dead.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayerTakeDamage(int EnemyDamage)
    {
        PlayerHP -= EnemyDamage;
        playerHealthBar.SetCurrentHP(PlayerHP);
        CheckPlayerDeath();    //ineffective should only be called when get damage 
        
    }

  

}

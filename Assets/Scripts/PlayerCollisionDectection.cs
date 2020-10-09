using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDectection : MonoBehaviour
{

    int goldQuantity;
    public int KarmaQuantity=10;

    PlayerStat playerStat;
    PlayerCurrency playerCurrency;
    playerUI playerUI;

   

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        playerCurrency = GetComponent<PlayerCurrency>();
        playerUI = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").GetComponent<playerUI>();
     
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Loot/Gold")) 
        {
            goldQuantity = UnityEngine.Random.Range(10, 100);//TODO: create a range of gold drop for different enemy and put here
            playerCurrency.playerGold += goldQuantity;
            playerUI.MoneyText();
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("Loot/Karma"))
        {
            playerCurrency.playerKarma += KarmaQuantity;
            Destroy(collision.gameObject);
            playerUI.SetKarmaValue();
            playerUI.KarmaText();
        }
            else if(collision.CompareTag("Loot/UpgradeDrop")) {
            playerCurrency.playerUpgrade++;
            Destroy(collision.gameObject);
            playerUI.UpgradeText();
        }
            
        else if (collision.CompareTag("Loot/HPDrop"))
        {
            playerStat.PlayerHP += 30;
            Destroy(collision.gameObject);
            playerUI.SetCurrentHP(playerStat.PlayerHP);
            
             }
        }
    }


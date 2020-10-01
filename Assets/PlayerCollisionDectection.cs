using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDectection : MonoBehaviour
{

    int goldQuantity;
    public int KarmaQuantity=10;

    PlayerStat playerStat;
    PlayerCurrency playerCurrency;
    GameObject playerUI;

    KarmaBar karmaBar;
    KarmaCount karmaCount;
    GoldCount goldCount;
    PlayerHealthBar healthBar;
    UpgradeCount upgradeCount;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        playerCurrency = GetComponent<PlayerCurrency>();
        playerUI = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").gameObject;
        goldCount = playerUI.GetComponent<GoldCount>();
        karmaBar = playerUI.GetComponent<KarmaBar>();
        karmaCount = playerUI.GetComponent<KarmaCount>();
        healthBar = playerUI.GetComponent<PlayerHealthBar>();
        upgradeCount = playerUI.GetComponent<UpgradeCount>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Loot/Gold")) 
        {
            goldQuantity = UnityEngine.Random.Range(10, 100);//TODO: create a range of gold drop for different enemy and put here
            playerCurrency.playerGold += goldQuantity;
            // goldCount.TextUpdate();
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("Loot/Karma"))
        {
            playerCurrency.playerKarma += KarmaQuantity;
            Destroy(collision.gameObject);
            karmaBar.SetValue();
            karmaCount.Updatetext();
        }
            else if(collision.CompareTag("Loot/UpgradeDrop")) {
            playerCurrency.playerUpgrade++;
            Destroy(collision.gameObject);
            upgradeCount.UpdateText();
        }
            
        else if (collision.CompareTag("Loot/HPDrop"))
        {
            playerStat.PlayerHP += 30;
            Destroy(collision.gameObject);
            healthBar.SetCurrentHP(playerStat.PlayerHP);
            
             }
        }
    }


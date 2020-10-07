using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerCurrency : MonoBehaviour
{
    public GameMaster gm;
    //public GameObject Player;
    //public Image prompt;
    public Text moneyText, upgradeText, karmaText;
    public Slider karmaBar;
    public int playerKarma, playerGold, playerUpgrade;
    Transform playerUI;
    void Awake()
    {
    
        LoadSaveFile();
       
    }
    private void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI");
    

        moneyText = playerUI.Find("moneyText").GetComponent<Text>();

        upgradeText = playerUI.Find("upgradeText").GetComponent<Text>();

        karmaText = playerUI.Find("karmaBar").Find("karmaText").GetComponent<Text>();

        karmaBar = playerUI.Find("karmaBar").GetComponent<Slider>();

        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<GameMaster>();


        /*karmaBar.maxValue = 1000;
        if (prompt != null)
         {
             prompt.enabled = false;
         }*/

        {
            // prompt.enabled = false;
        }
    }

    void LoadSaveFile()
    {
        CurrencyData data = SaveManager.LoadCurrency();

        playerKarma = data.playerKarma;
        playerGold = data.playerMoney;
        playerUpgrade = data.speedUpgrades;

       /* updateCurrency(playerGold);
        updateKarma(playerKarma);
        updateUpgrades(playerUpgrade);*/
    }

    public void updateCurrency(int amount)
    {
        playerGold += amount;
        if(playerGold <= 0)
        {
            playerGold = 0;
        }
        
       // moneyText.text = playerGold.ToString();
    }

    public void updateKarma(int amount)
    {
        playerKarma += amount;
     //   karmaBar.value = playerKarma;
      //  karmaText.text = playerKarma.ToString();
    }

    public void updateUpgrades(int amount)
    {
        playerUpgrade += amount;
        if(playerUpgrade <= 0)
        {
            playerUpgrade = 0;
        }
       // upgradeText.text = playerUpgrade.ToString();

//      upgradeText.text = playerUpgrade.ToString();
    }
}

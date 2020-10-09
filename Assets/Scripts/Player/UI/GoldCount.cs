using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldCount : MonoBehaviour
{
    
    private PlayerCurrency playerCurrency;
    public Text moneyText;

      
    
    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        moneyText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("moneyText").GetComponent<Text>();
        MoneyText();

    }


 


    public void MoneyText()

    {
        moneyText.text = playerCurrency.playerGold.ToString();
        moneyText.color = Color.red;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    public Text moneyText;

      
    
    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        moneyText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("moneyText").GetComponent<Text>();


        TextUpdate();


    }


   public void TextUpdate()
    {
        moneyText.text = playerCurrency.playerGold.ToString();
    }
}

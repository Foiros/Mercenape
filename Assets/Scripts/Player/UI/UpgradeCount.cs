using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UpgradeCount : MonoBehaviour
{
    private PlayerCurrency playerCurrency;
    public Text upgradeText;


    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        
        upgradeText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("upgradeText").GetComponent<Text>();



        TextUpdate();// update the UI when start the game

    }


    // Update is called once per frame
    public void TextUpdate()
    {
        upgradeText.text = playerCurrency.playerUpgrade.ToString();
    }
}

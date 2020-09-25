﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 11.9.2020: This is just a temprory script for testing purposes. I need to have some kinda of temporary currency in place to test buying part of the component unlocking. 
public class Money : MonoBehaviour
{
    private PlayerCurrency playerCurrency;
    
    private int currency;
    private int newCurrency;

    private Text currencyHolder;
    private Text upgradeHolderMainScreen;
    

    void Awake()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        
        currencyHolder = GameObject.FindGameObjectWithTag("Money").GetComponentInChildren<Text>();
        upgradeHolderMainScreen = GameObject.FindGameObjectWithTag("UpgradesMainScreen").GetComponentInChildren<Text>();
    }

    void Start()
    {
        SetStartingCurrency();
    }

    // Public function for the other scripts to get how much player has money.
    public int GetCurrentCurrency()
    {
        return currency;
    }

    // Function to set the starting money.
    void SetStartingCurrency()
    {
        currency = playerCurrency.playerGold;

        currencyHolder.text = "Money: " + currency;
        upgradeHolderMainScreen.text = "Speed Upgrades: " + playerCurrency.playerUpgrade;
    }
    
    // Function for changing amount of money, for example when buying components. 
    public void ChangeCurrencyAmount(int change)
    {
        newCurrency = currency - change;

        currency = newCurrency;

        playerCurrency.playerGold = currency;

        currencyHolder.text = "Money: " + currency;
    }

    public void ChangeUpgradeAmount()
    {
        upgradeHolderMainScreen.text = "Speed Upgrades: " + playerCurrency.playerUpgrade;
    }
}

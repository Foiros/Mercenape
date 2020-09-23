using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 11.9.2020: This is just a temprory script for testing purposes. I need to have some kinda of temporary currency in place to test buying part of the component unlocking. 
public class Money : MonoBehaviour
{
    private int currency;
    private int newCurrency;

    private Text currencyHolder;

    void Awake()
    {
        currencyHolder = GameObject.FindGameObjectWithTag("Money").GetComponentInChildren<Text>();
    }

    void Start()
    {
        GetStartingCurrency();
    }

    // Public function for the other scripts to get how much player has money.
    public int GetCurrentCurrency()
    {
        return currency;
    }

    // Function to set the starting money.
    void GetStartingCurrency()
    {
        currency = 200;

        currencyHolder.text = "Money: " + currency;
    }
    
    // Function for changing amount of money, for example when buying components. 
    public void ChangeCurrencyAmount(int change)
    {
        newCurrency = currency - change;

        currency = newCurrency;

        currencyHolder.text = "Money: " + currency;
    }
}

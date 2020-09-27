﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Created by Arttu Paldán 11.9.2020: This script will handle buying or unlocking component pieces.
public class BuyWeapons : MonoBehaviour
{
    private List<AbstractWeapon> weapons;

    private Money money;
    private WeaponStates weaponStates;

    private int weaponID;
    private int weaponCost;

    public Image[] weaponImagesHolder;
    public Image[] ownedWeapons;

    public GameObject buyWeaponScreen;
    private Text weaponName;
    private Text weaponDescription;
    private Text weaponCostText;
    private Image weaponImageBuyScreen;

    private bool cantBuy;

    private float counterStart;
    private float counterEnd;
    private float originalStart;

    private string buttonName;

    void Awake()
    {
        money = GetComponent<Money>();
        weaponStates = GetComponent<WeaponStates>();

        weaponName = GameObject.FindGameObjectWithTag("WeaponName").GetComponent<Text>();
        weaponDescription = GameObject.FindGameObjectWithTag("WeaponDescription").GetComponent<Text>();
        weaponCostText = GameObject.FindGameObjectWithTag("WeaponCost").GetComponent<Text>();
        weaponImageBuyScreen = GameObject.FindGameObjectWithTag("BuyScreenWeaponImage").GetComponent<Image>();

        SetWeaponsHolder();

        buyWeaponScreen.SetActive(false);

        weaponID = -1;

        originalStart = counterStart;
    }

    void Update()
    {
        if (cantBuy)
        {
            CantBuyCounter();
        }
    }

    void SetWeaponsHolder()
    {
        for (int i = 0; i < weaponImagesHolder.Length; i++)
        {
            weaponImagesHolder[i].sprite = weapons[i].GetWeaponImage();
        }
    }

    // Function to open the buy component screen.
    void OpenWeapon()
    {
        buyWeaponScreen.SetActive(true);

        SetBuyWeaponScreen();
    }

    // Function to close the buy component screen.
    public void CloseWeapon()
    {
        buyWeaponScreen.SetActive(false);

        weaponID = -1;
    }

    // Sets the sprites and texts in the buy screen. These things are gotten from the abstract components
    void SetBuyWeaponScreen()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        weaponImageBuyScreen.sprite = weaponsArray.GetWeaponImage();
        weaponName.text = weaponsArray.GetName();
        weaponDescription.text = weaponsArray.GetDescription();
        weaponCost = weaponsArray.GetCost();
        weaponCostText.text = "Cost: " + weaponCost;
    }

    // Function for buying the components.
    public void Buy()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        int currency = money.GetCurrentCurrency();

        if(currency >= weaponCost)
        {
            money.ChangeCurrencyAmount(weaponCost);
            weaponStates.WhatWeaponWasBought(weaponID);
           
            weaponImagesHolder[weaponID].enabled = false;
            ownedWeapons[weaponID].sprite = weaponsArray.GetWeaponImage();

            SaveManager.SaveWeapons(weaponStates);
            buyWeaponScreen.SetActive(false);
        }
        else
        {
            cantBuy = true;
        }
    }

    // Function that announces, that player can't buy component and keeps this message going for couple of frames. 
    void CantBuyCounter()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        weaponDescription.text = "Don't have enough money for this component";

        counterEnd = 3;

        counterStart += Time.deltaTime;
        if (counterStart >= counterEnd)
        {
            cantBuy = false;
            weaponDescription.text = weaponsArray.GetDescription();
            counterStart = originalStart;
        }
    }

    // Button function that recognizes, which button has been pressed and based on this gives out the weaponID we need to execute rest of the code. 
    public void WeaponButton()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;

        if(buttonName == "WeaponA")
        {
            weaponID = weapons[0].GetID();
        }
        else if(buttonName == "WeaponB")
        {
            weaponID = weapons[1].GetID();
        }
        else if(buttonName == "WeaponC")
        {
            weaponID = weapons[2].GetID();
        }
        else if (buttonName == "WeaponD") 
        {
            weaponID = weapons[3].GetID();
        }

        OpenWeapon();
    }
    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

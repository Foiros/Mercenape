using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán on 28.9.2020: Static class that handles the heavier method operations of BuyWeapons script. Not super necessary, mainly just a experiment of mine to see how static classes work.
// Really this only cleans up the BuyWeapon script by hiding these heavy operations to the background.
public static class BuyOperations
{
    // Gets the important components for BuyWeapon script, like for example the other scripts used by BuyWeapons.
    public static void SetUpImportantComponents(BuyWeapons buy, float start)
    {
        Money money = buy.GetComponent<Money>();
        WeaponStates weaponStates = buy.GetComponent<WeaponStates>();

        buy.SetMoney(money);
        buy.SetWeaponStates(weaponStates);
        
        Text weaponName = GameObject.FindGameObjectWithTag("WeaponName").GetComponent<Text>();
        Text weaponDescription = GameObject.FindGameObjectWithTag("WeaponDescription").GetComponent<Text>();
        Text weaponCostText = GameObject.FindGameObjectWithTag("WeaponCost").GetComponent<Text>();
        Image weaponImageBuyScreen = GameObject.FindGameObjectWithTag("BuyScreenWeaponImage").GetComponent<Image>();

        buy.SetWeaponNameText(weaponName);
        buy.SetWeaponDescText(weaponDescription);
        buy.SetWeaponCostText(weaponCostText);
        buy.SetBuyScreenWeaponImage(weaponImageBuyScreen);

        buy.SetWeaponID(-1);
        buy.SetOriginalCounterStart(start);
    }

    // Sets up the buyweapon screen in game.
    public static void SetBuyScreen(List<AbstractWeapon> weaponsList, int id, Image weaponImage, Text weaponName, Text weaponDescription, Text weaponCostText)
    {
        int weaponCost = weaponsList[id].GetCost();

        weaponImage.sprite = weaponsList[id].GetWeaponImage();
        weaponName.text = weaponsList[id].GetName();
        weaponDescription.text = weaponsList[id].GetDescription();
        weaponCostText.text = "Cost: " + weaponCost;
    }

    // Handles the buying action.
    public static void BuyWeapon(BuyWeapons buy, WeaponStates weaponStates, Money money, List<AbstractWeapon> weaponsList, int id)
    {
        int weaponCost = weaponsList[id].GetCost();
        int currency = money.GetCurrentCurrency();

        if (currency >= weaponCost)
        {
            money.ChangeCurrencyAmount(weaponCost);
            weaponStates.WhatWeaponWasBought(id);

            buy.weaponImagesHolder[id].enabled = false;
            buy.ownedWeapons[id].sprite = weaponsList[id].GetWeaponImage();

            SaveManager.SaveWeapons(weaponStates);
            buy.buyWeaponScreen.SetActive(false);
        }
        else
        { 
            buy.SetCantBuy(true);
        }
    }

    // Checks which button was pressed and returns id for the Buyweapon script.
    public static void WeaponButtonPress(BuyWeapons buy, string button, List<AbstractWeapon> weapons)
    {
        int id;

        if (button == "WeaponA")
        {
            id = weapons[0].GetID();
            buy.SetWeaponID(id);
        }
        else if (button== "WeaponB")
        {
            id = weapons[1].GetID();
            buy.SetWeaponID(id);
        }
        else if (button == "WeaponC")
        {
            id = weapons[2].GetID();
            buy.SetWeaponID(id);
        }
        else if (button == "WeaponD")
        {
            id = weapons[3].GetID();
            buy.SetWeaponID(id);
        }
    }
}

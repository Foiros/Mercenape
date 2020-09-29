using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Created by Arttu Paldán 16.9.2020: This script allows the player to place upgrades into his weapon. 
public class UseUpgrades : MonoBehaviour
{
    private WeaponStates weaponStates;
    private Money money;
    private WeaponStats weaponStats;
    private PlayerCurrency playerCurrency;
   
    private List<AbstractWeapon> weapons;
    private List<AbstractUpgrades> upgrades;

    private int weaponID;
    private int upgradeID;
    private int upgradeCost;

    private string weaponButtonName;
    private string upgradeButtonName;
    private string arrowButtonName;

    private Text weaponName;
    private Text weaponDescription;
    private Text weaponSpeedText;
    private Text weaponWeightText;
    private Text weaponImpactDamageText;
    private Text weaponCostText;
    private Image weaponImage;

    private Text upgradeHolderUpgradeScreen;
    private Text upgradeName;
    private Text upgradeDescription;
    private Image upgradeImage;

    public Text[] amountTexts;
    public Image[] upgradeImagesHolder;
    
    public GameObject upgradeMenu;
    public GameObject upgradeComponentScreen;

    void Awake()
    {
        GetRequiredObjects();
        SetScreensInactive();
    }

    void GetRequiredObjects()
    {
        weaponStates = GetComponent<WeaponStates>();
        money = GetComponent<Money>();
        weaponStats = GetComponent<WeaponStats>();
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        weaponName = GameObject.FindGameObjectWithTag("UpgradeScreenWeaponName").GetComponent<Text>();
        weaponDescription = GameObject.FindGameObjectWithTag("UpgradeScreenWeaponDescription").GetComponent<Text>();
        weaponSpeedText = GameObject.FindGameObjectWithTag("WeaponSpeed").GetComponent<Text>();
        weaponWeightText = GameObject.FindGameObjectWithTag("WeaponWeight").GetComponent<Text>();
        weaponImpactDamageText = GameObject.FindGameObjectWithTag("ImpactDamage").GetComponent<Text>();
        weaponCostText = GameObject.FindGameObjectWithTag("UpgradeCost").GetComponent<Text>();
        weaponImage = GameObject.FindGameObjectWithTag("UpgradeScreenWeaponImage").GetComponent<Image>();

        upgradeName = GameObject.FindGameObjectWithTag("UpgradeName").GetComponent<Text>();
        upgradeDescription = GameObject.FindGameObjectWithTag("UpgradeDescription").GetComponent<Text>();
        upgradeImage = GameObject.FindGameObjectWithTag("UpgradeImage").GetComponent<Image>();
        upgradeHolderUpgradeScreen = GameObject.FindGameObjectWithTag("UpgradesUpgradeScreen").GetComponentInChildren<Text>();
    }

    void SetScreensInactive()
    {
        upgradeMenu.SetActive(false);
        upgradeComponentScreen.SetActive(false);
    }

    // Button function, that detects which button has been pressed and returns ID based on that. 
    public void OpenUpgradeMenu()
    {
        weaponButtonName = EventSystem.current.currentSelectedGameObject.name;

        List<bool> ownedWeaponsList = weaponStates.GetOwnedWeapons();

        if(weaponButtonName == "UpgradeButton1" && ownedWeaponsList[0])
        {
            weaponID = weapons[0].GetID();
           
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton2" && ownedWeaponsList[1])
        {
            weaponID = weapons[1].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton3" && ownedWeaponsList[2])
        {
            weaponID = weapons[2].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton4" && ownedWeaponsList[3])
        {
            weaponID = weapons[3].GetID();
            SetUpUpgradeScreen();
        }
    }
    
    // This function sets up the upgrade screen based on what weapon player has chosen from his owned weapons inventory. 
    void SetUpUpgradeScreen()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        upgradeMenu.SetActive(true);

        weaponName.text = weaponsArray.GetName();
        weaponDescription.text = weaponsArray.GetDescription();
        weaponCostText.text = "Upgrade Cost: " + upgradeCost;
        weaponImage.sprite = weaponsArray.GetWeaponImage();

        upgradeHolderUpgradeScreen.text = "Speed Upgrades: " + playerCurrency.playerUpgrade;

        UpdateWeaponStats();

        for (int i = 0; i < upgradeImagesHolder.Length; i++)
        {
            upgradeImagesHolder[i].sprite = upgrades[i].GetUpgradeImage();
        }

        for (int i = 0; i < amountTexts.Length; i++)
        {
            amountTexts[i].text = "0";
        }
    }

    // Button function for opening an screen that will explain, what the upgrade does when put into the weapon. 
    public void OpenUpgradeComponent()
    {
        upgradeButtonName = EventSystem.current.currentSelectedGameObject.name;

        if (upgradeButtonName == "UpgradeComponent1")
        {
            upgradeID = upgrades[0].GetID();
        }
        else if (upgradeButtonName == "UpgradeComponent2")
        {
            upgradeID = upgrades[1].GetID();
        }
        
        SetUpUpgradeComponentScreen();
    }

    // Sets up the upgrade explanation screen. 
    void SetUpUpgradeComponentScreen()
    {
        AbstractUpgrades upgradesArray = upgrades[upgradeID];

        upgradeComponentScreen.SetActive(true);

        upgradeName.text = upgradesArray.GetName();
        upgradeDescription.text = upgradesArray.GetDescription();
        upgradeImage.sprite = upgradesArray.GetUpgradeImage();
    }

    // Function for closing the upgrade screen.
    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        weaponID = -1;
        weaponStats.savedAmountOfSpeed = 0;
        weaponStats.savedAmountOfWeight = 0;
    }

    //Function for closing the componentscreen.
    public void CloseUpgradeComponentMenu()
    {
        upgradeComponentScreen.SetActive(false);
        upgradeID = -1;
    }

    // Button function for selecting the amount of upgrades to be inserted into the weapon.
    public void SelectUpgradeComponentsAmount()
    {
        arrowButtonName = EventSystem.current.currentSelectedGameObject.name;

        if (arrowButtonName == "Arrow1" && playerCurrency.playerUpgrade > 0)
        {
            upgradeID = upgrades[0].GetID();

            weaponStats.amountOfSpeed++;
            amountTexts[0].text = "" + weaponStats.amountOfSpeed;
            upgradeCost = upgrades[upgradeID].GetUpgradeCost() * weaponStats.amountOfSpeed;
            playerCurrency.playerUpgrade--;
        }
        else if (arrowButtonName == "Arrow2" && weaponStats.amountOfSpeed > 0)
        {
            upgradeID = upgrades[0].GetID();

            weaponStats.amountOfSpeed--;
            amountTexts[0].text = "" + weaponStats.amountOfSpeed;
            upgradeCost = upgrades[upgradeID].GetUpgradeCost() * weaponStats.amountOfSpeed;
            playerCurrency.playerUpgrade++;
        }
        else if (arrowButtonName == "Arrow3" && playerCurrency.playerUpgrade > 0)
        {
            upgradeID = upgrades[1].GetID();

            weaponStats.amountOfWeight++;
            amountTexts[1].text = "" + weaponStats.amountOfWeight;
            upgradeCost = upgrades[upgradeID].GetUpgradeCost() * weaponStats.amountOfWeight;
            playerCurrency.playerUpgrade--;
        }
        else if (arrowButtonName == "Arrow4" && weaponStats.amountOfWeight > 0)
        {
            upgradeID = upgrades[1].GetID();

            weaponStats.amountOfWeight--;
            amountTexts[1].text = "" + weaponStats.amountOfWeight;
            upgradeCost = upgrades[upgradeID].GetUpgradeCost() * weaponStats.amountOfWeight;
            playerCurrency.playerUpgrade++;
        }

        weaponCostText.text = "Upgrade Cost: " + upgradeCost;

        money.ChangeUpgradeAmount();
        upgradeHolderUpgradeScreen.text = "Speed Upgrades: " + playerCurrency.playerUpgrade;

        UpdateWeaponStats();
    }

    // Function for updating the info on weapon stats in upgrade screen.
    void UpdateWeaponStats()
    {
        weaponStats.SetRequestFromUpgrades(true);
        weaponStats.CalculateStats();

        weaponSpeedText.text = "Speed: " + weaponStats.GetSpeed();
        weaponWeightText.text = " Weight: " + weaponStats.GetWeight();
        weaponImpactDamageText.text = " Impact Damage: " + weaponStats.GetImpactDamage();
    }

    // Confirm button function for confirming the updates. 
    public void ConfirmUpgrade()
    {
        int currency = money.GetCurrentCurrency();

        if(currency >= upgradeCost)
        {
            money.ChangeCurrencyAmount(upgradeCost);
            weaponStates.WhatWeaponWasUpgraded(weaponID);
            
            upgradeCost = 0;
            weaponCostText.text = "Upgrade Cost: " + upgradeCost;

            UpdateWeaponStats();
            weaponStats.SaveStats();
            
            weaponStats.amountOfSpeed = 0;
            weaponStats.amountOfWeight = 0;

            weaponStats.SetWeight(0);
            weaponStats.SetSpeed(0);
            weaponStats.SetImpactDamage(0);

            for (int i = 0; i < amountTexts.Length; i++)
            {
                amountTexts[i].text = "0";
            }

            SaveManager.SaveWeapons(weaponStates);
            SaveManager.SaveCurrency(playerCurrency);
        }
    }

    public int GetWeaponID() { return weaponID; }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
    public void  SetUpgradeList(List<AbstractUpgrades> list) { upgrades = list; }
}

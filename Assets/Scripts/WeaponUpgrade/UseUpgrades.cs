using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Created by Arttu Paldán 16.9.2020: This script allows the player to place upgrades into his weapon. 
public class UseUpgrades : MonoBehaviour
{
    private WeaponStates weaponStates;
    private AssetManager assetManager;
    private Money money;
    private WeaponStats weaponStats;
    private PlayerCurrency playerCurrency;
    
    private AbstractWeapon[] weapons;
    private AbstractUpgrades[] upgrades;

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
        weaponStates = GetComponent<WeaponStates>();
        assetManager = GetComponent<AssetManager>();
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

        upgradeMenu.SetActive(false);
        upgradeComponentScreen.SetActive(false);

        SetUpWeaponsArray();
        SetUpUpgradesArray();
    }
    
    // Function for setting up the abstract weapons in this script and putting them into an array. 
     void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, 20, 0.3f, 3f, assetManager.weaponImages[0], null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, 30, 0.3f, 2f, assetManager.weaponImages[1], null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, 10, 0.3f, 1f, assetManager.weaponImages[2], null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, 20, 0.3f, 5f, assetManager.weaponImages[3], null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Function sets up the upgrades array for this script to use.
    void SetUpUpgradesArray()
    {
        TestUpgrade1 testUpgrade1 = new TestUpgrade1("Speed Upgrade", "Increases the speed of your attacks", 0, 25, assetManager.upgradeImages[0]);
        TestUpgrade2 testUpgrade2 = new TestUpgrade2("Weigh Upgrade", "Increases the weight of your weapon", 1, 25, assetManager.upgradeImages[1]);

        upgrades = new AbstractUpgrades[] { testUpgrade1, testUpgrade2 };
    }

    // Button function, that detects which button has been pressed and returns ID based on that. 
    public void OpenUpgradeMenu()
    {
        weaponButtonName = EventSystem.current.currentSelectedGameObject.name;

        if(weaponButtonName == "UpgradeButton1" && weaponStates.ownsWeapon1)
        {
            weaponID = weapons[0].GetID();
           
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton2" && weaponStates.ownsWeapon2)
        {
            weaponID = weapons[1].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton3" && weaponStates.ownsWeapon3)
        {
            weaponID = weapons[2].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton4" && weaponStates.ownsWeapon4)
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
        
        for(int i = 0; i < upgradeImagesHolder.Length; i++)
        {
            upgradeImagesHolder[i].sprite = upgrades[i].GetUpgradeImage();
        }

        for(int i = 0; i < amountTexts.Length; i++)
        {
            amountTexts[i].text = "0"; 
        }

        UpdateWeaponStats();
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

            weaponStates.WhatWeaponWasUpgraded(weaponID);

            SaveManager.SaveCurrency(playerCurrency);
        }
    }

    public int GetWeaponID() { return weaponID; }
}

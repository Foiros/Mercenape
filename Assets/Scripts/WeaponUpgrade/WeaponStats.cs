using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 23.9.2020: This script handles weapon stat calculations. 
public class WeaponStats : MonoBehaviour
{
    private UseUpgrades useUpgrades;
    private WeaponStates weaponStates;
    
    private AbstractWeapon[] weapons;

    public int amountOfSpeed;
    public int savedAmountOfSpeed;
    public int amountOfWeight;
    public int savedAmountOfWeight;

    private int weaponWeight;
    private int weightEffect;
    private int weaponSpeed;
    private int weaponImpactDamage;

    private int actualWeaponWeight;
    private int actualWeaponSpeed;
    private int actualWeaponImpactDamage;

    void Awake()
    {
        useUpgrades = GetComponent<UseUpgrades>();
        weaponStates = GetComponent<WeaponStates>();

        SetUpWeaponsArray();
    }

    // Just as everywhere else, this function sets the weapons array.
    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, 20, null, null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, 30, null, null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, 10, null, null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, 20, null, null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Public function that other scripts can call to handle the weapon stat calculations.
    public void CalculateStats()
    {
        if(useUpgrades.weaponID == 0 && weaponStates.weapon1HasBeenUpgraded)
        {
            CalculateWithSavedStats();
        }
        else if (useUpgrades.weaponID == 1 && weaponStates.weapon2HasBeenUpgraded)
        {
            CalculateWithSavedStats();
        }
        else if (useUpgrades.weaponID == 2 && weaponStates.weapon3HasBeenUpgraded)
        {
            CalculateWithSavedStats();
        }
        else if (useUpgrades.weaponID == 3 && weaponStates.weapon4HasBeenUpgraded)
        {
            CalculateWithSavedStats();
        }
        else
        {
            CalculateNormally();
        }
    }

    // Function for calculating stats in situations, where player has upgraded the weapon.
    void CalculateWithSavedStats()
    {
        AbstractWeapon weaponsArray = weapons[useUpgrades.weaponID];

        LoadSaveFiles();

        weaponWeight = weaponsArray.GetWeight();
        weaponSpeed = weaponsArray.GetSpeed();
        weaponImpactDamage = weaponsArray.GetImpactDamage();

        actualWeaponWeight = weaponWeight * savedAmountOfWeight;

        GetWeightEffect();

        actualWeaponSpeed = weaponSpeed * savedAmountOfSpeed - weightEffect;

        actualWeaponImpactDamage = weaponImpactDamage + weightEffect;

        savedAmountOfWeight = 0;
        savedAmountOfSpeed = 0;
    }

    // Function for calculating weapon stats when changes have not been made or when player upgrades the weapon.
    void CalculateNormally()
    {
        AbstractWeapon weaponsArray = weapons[useUpgrades.weaponID];

        weaponWeight = weaponsArray.GetWeight();
        weaponSpeed = weaponsArray.GetSpeed();
        weaponImpactDamage = weaponsArray.GetImpactDamage();


        if (amountOfWeight > 0)
        {
            actualWeaponWeight = weaponWeight * amountOfWeight;
        }
        else
        {
            actualWeaponWeight = weaponWeight;
        }

        GetWeightEffect();

        
        if (amountOfSpeed > 0)
        {
            actualWeaponSpeed = weaponSpeed * amountOfSpeed - weightEffect;
        }
        else
        {
            actualWeaponSpeed = weaponSpeed - weightEffect;
        }

        actualWeaponImpactDamage =  weaponImpactDamage + weightEffect;
    }

    // Switch lopp function that sets the weight effect. I really need to find a better way to calculate this. The whole math behind this system requires work. 
    void GetWeightEffect()
    {
        switch (actualWeaponWeight)
        {
            case 0:
                weightEffect = 0;
                break;

            case 10:
                weightEffect = 10;
                break;

            case 20:
                weightEffect = 20;
                break;

            case 30:
                weightEffect = 30;
                break;

            case 40:
                weightEffect = 40;
                break;

            case 50:
                weightEffect = 50;
                break;

            case 60:
                weightEffect = 60;
                break;

            case 70:
                weightEffect = 70;
                break;

            case 80:
                weightEffect = 80;
                break;

            case 90:
                weightEffect = 90;
                break;

            case 100:
                weightEffect = 100;
                break;
        }
    }

    // Function for saving the amount of upgrades.
    public void SaveStats()
    {
        switch (useUpgrades.weaponID)
        {
            case 0:
                weaponStates.savedWeightAmount1 = weaponStates.savedWeightAmount1 + amountOfWeight;
                weaponStates.savedSpeedAmount1 = weaponStates.savedSpeedAmount1 + amountOfSpeed;
                break;

            case 1:
                weaponStates.savedWeightAmount2 = weaponStates.savedWeightAmount2 + amountOfWeight;
                weaponStates.savedSpeedAmount2 = weaponStates.savedSpeedAmount2 + amountOfSpeed;
                break;

            case 2:
                weaponStates.savedWeightAmount3 = weaponStates.savedWeightAmount3 + amountOfWeight;
                weaponStates.savedSpeedAmount3 = weaponStates.savedSpeedAmount3 + amountOfSpeed;
                break;

            case 3:
                weaponStates.savedWeightAmount4 = weaponStates.savedWeightAmount4 + amountOfWeight;
                weaponStates.savedSpeedAmount4 = weaponStates.savedSpeedAmount4 + amountOfSpeed;
                break;
        }
    }

    // Function for loading the amount of upgrades. 
    void LoadSaveFiles()
    {
        switch (useUpgrades.weaponID)
        {
            case 0:
                savedAmountOfWeight = weaponStates.savedWeightAmount1;
                savedAmountOfSpeed = weaponStates.savedSpeedAmount1;
                break;

            case 1:
                savedAmountOfWeight = weaponStates.savedWeightAmount2;
                savedAmountOfSpeed = weaponStates.savedSpeedAmount2;
                break;

            case 3:
                savedAmountOfWeight = weaponStates.savedWeightAmount3;
                savedAmountOfSpeed = weaponStates.savedSpeedAmount3;
                break;

            case 4:
                savedAmountOfWeight = weaponStates.savedWeightAmount4;
                savedAmountOfSpeed = weaponStates.savedSpeedAmount4;
                break;
        }
    }

    // Fetch functions for other scripts to get their hands on these private values.
    public int GetWeight() { return actualWeaponWeight; }
    public int GetSpeed() { return actualWeaponSpeed; }
    public int GetImpactDamage() { return actualWeaponImpactDamage; }

    public void SetWeight(int weight) { actualWeaponWeight = weight; }
    public void SetSpeed(int speed) { actualWeaponSpeed = speed; }
    public void SetImpactDamage(int damage) { actualWeaponImpactDamage = damage; }
}

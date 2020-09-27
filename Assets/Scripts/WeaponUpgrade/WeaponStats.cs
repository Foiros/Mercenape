using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 23.9.2020: This script handles weapon stat calculations. 
public class WeaponStats : MonoBehaviour
{
    private UseUpgrades useUpgrades;
    private WeaponStates weaponStates;
    private SetActualWeapon setActualWeapon;
    
    private List<AbstractWeapon> weapons;

    private bool requestCameFromUseUpgrades;
    private bool requestCameFromSetActualWeapon;
    
    private int weaponID;

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
        setActualWeapon = GetComponent<SetActualWeapon>();
    }

    // Public function that other scripts can call to handle the weapon stat calculations.
    public void CalculateStats()
    {
        List<bool> upgradedWeaponsList = weaponStates.GetUpgradedWeapons();

        if (requestCameFromUseUpgrades)
        {
            weaponID = useUpgrades.GetWeaponID();
        }
        else if (requestCameFromSetActualWeapon)
        {
            weaponID = setActualWeapon.GetChosenID();
        }
        
        if(weaponID == 0 && upgradedWeaponsList[0])
        {
            CalculateWithSavedStats();
        }
        else if (weaponID == 1 && upgradedWeaponsList[1])
        {
            CalculateWithSavedStats();
        }
        else if (weaponID == 2 && upgradedWeaponsList[2])
        {
            CalculateWithSavedStats();
        }
        else if (weaponID == 3 && upgradedWeaponsList[3])
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
        AbstractWeapon weaponsArray = weapons[weaponID];

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
        AbstractWeapon weaponsArray = weapons[weaponID];

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
        List<int> savedWeightsList = weaponStates.GetSavedWeights();
        List<int> savedSpeedsList = weaponStates.GetSavedSpeeds();

        switch (weaponID)
        {
            case 0:
                savedWeightsList[0] = savedWeightsList[0] + amountOfWeight;
                savedSpeedsList[0] = savedSpeedsList[0] + amountOfSpeed;
                break;

            case 1:
                savedWeightsList[1] = savedWeightsList[1] + amountOfWeight;
                savedSpeedsList[1] = savedSpeedsList[1] + amountOfSpeed;
                break;

            case 2:
                savedWeightsList[2] = savedWeightsList[2] + amountOfWeight;
                savedSpeedsList[2] = savedSpeedsList[2] + amountOfSpeed;
                break;

            case 3:
                savedWeightsList[3] = savedWeightsList[3] + amountOfWeight;
                savedSpeedsList[3] = savedSpeedsList[3] + amountOfSpeed;
                break;
        }
    }

    // Function for loading the amount of upgrades. 
    void LoadSaveFiles()
    {
        List<int> savedWeightsList = weaponStates.GetSavedWeights();
        List<int> savedSpeedsList = weaponStates.GetSavedSpeeds();

        switch (weaponID)
        {
            case 0:
                savedAmountOfWeight = savedWeightsList[0];
                savedAmountOfSpeed = savedSpeedsList[0];
                break;

            case 1:
                savedAmountOfWeight = savedWeightsList[1];
                savedAmountOfSpeed = savedSpeedsList[1];
                break;

            case 3:
                savedAmountOfWeight = savedWeightsList[2];
                savedAmountOfSpeed = savedSpeedsList[2];
                break;

            case 4:
                savedAmountOfWeight = savedWeightsList[3];
                savedAmountOfSpeed = savedSpeedsList[3];
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

    public void SetRequestFromUpgrades(bool request) { requestCameFromUseUpgrades = request; }
    public void SetRequestFromActualWeapon(bool request) { requestCameFromSetActualWeapon = request; }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

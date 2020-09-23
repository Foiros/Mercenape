using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 23.9.2020: 
public class WeaponStats : MonoBehaviour
{
    private UseUpgrades useUpgrades;
    private WeaponStates weaponStates;
    
    private AbstractWeapon[] weapons;

    public int amountOfSpeed;
    public int amountOfWeight;

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

    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, 20, null, null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, 30, null, null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, 10, null, null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, 20, null, null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

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

    void CalculateWithSavedStats()
    {
        AbstractWeapon weaponsArray = weapons[useUpgrades.weaponID];

        weaponWeight = weaponsArray.GetWeight();
        weaponSpeed = weaponsArray.GetSpeed();
        weaponImpactDamage = weaponsArray.GetImpactDamage();

        actualWeaponWeight = weaponWeight * weaponStates.savedWeightAmount;

        GetWeightEffect();

        actualWeaponSpeed = weaponSpeed * weaponStates.savedSpeedAmount - weightEffect;

        actualWeaponImpactDamage = weaponImpactDamage + weightEffect;
    }

    void CalculateNormally()
    {
        AbstractWeapon weaponsArray = weapons[useUpgrades.weaponID];

        weaponWeight = weaponsArray.GetWeight();
        weaponSpeed = weaponsArray.GetSpeed();
        weaponImpactDamage = weaponsArray.GetImpactDamage();


        if (amountOfWeight > 0)
        {
            actualWeaponWeight = weaponWeight * amountOfWeight;
            weaponStates.savedWeightAmount = amountOfWeight;
        }
        else
        {
            actualWeaponWeight = weaponWeight;
        }

        GetWeightEffect();

        
        if (amountOfSpeed > 0)
        {
            actualWeaponSpeed = weaponSpeed * amountOfSpeed - weightEffect;
            weaponStates.savedSpeedAmount = amountOfSpeed;
        }
        else
        {
            actualWeaponSpeed = weaponSpeed - weightEffect;
        }

        actualWeaponImpactDamage =  weaponImpactDamage + weightEffect;
    }

    void GetWeightEffect()
    {
        switch (weaponWeight)
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
    public int GetWeight() { return actualWeaponWeight; }
    public int GetSpeed() { return actualWeaponSpeed; }
    public int GetImpactDamage() { return actualWeaponImpactDamage; }

    public void SetWeight(int weight) { actualWeaponWeight = weight; }
    public void SetSpeed(int speed) { actualWeaponSpeed = speed; }
    public void SetImpactDamage(int damage) { actualWeaponImpactDamage = damage; }
}

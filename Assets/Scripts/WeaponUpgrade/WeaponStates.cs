using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: A script that handles the ownership issues of this system.
public class WeaponStates: MonoBehaviour
{
    public int weaponID;

    public bool weapon1HasBeenUpgraded;
    public bool weapon2HasBeenUpgraded;
    public bool weapon3HasBeenUpgraded;
    public bool weapon4HasBeenUpgraded;

    public bool ownsWeapon1;
    public bool ownsWeapon2;
    public bool ownsWeapon3;
    public bool ownsWeapon4;

    public int savedWeightAmount1;
    public int savedWeightAmount2;
    public int savedWeightAmount3;
    public int savedWeightAmount4;
    
    public int savedSpeedAmount1;
    public int savedSpeedAmount2;
    public int savedSpeedAmount3;
    public int savedSpeedAmount4;

    void Awake()
    {
        LoadWeaponData();
    }

    // Switch loop function for setting the ownership status of a weapon.
    public void WhatWeaponWasBought(int id)
    {
        int weaponID = id;

        switch (weaponID)
        {
            case 0:
                ownsWeapon1 = true;
                break;

            case 1:
                ownsWeapon2 = true;
                break;

            case 2:
                ownsWeapon3 = true;
                break;

            case 3:
                ownsWeapon4 = true;
                break;
        }

        SaveManager.SaveWeapons(this);
    }

    // Switch loop function for setting the upgrade status of a weapon. 
    public void WhatWeaponWasUpgraded(int id)
    {
        int weaponID = id;
        
        switch (weaponID)
        {
            case 0:
                weapon1HasBeenUpgraded = true;
                break;

            case 1:
                weapon2HasBeenUpgraded = true;
                break;

            case 2:
                weapon3HasBeenUpgraded = true;
                break;

            case 3:
                weapon4HasBeenUpgraded = true;
                break;
        }

        SaveManager.SaveWeapons(this);
    }

    void LoadWeaponData()
    {
        WeaponsData data = SaveManager.LoadWeapons();

        weaponID = data.weaponID;

        weapon1HasBeenUpgraded = data.weapon1HasBeenUpgraded;
        weapon2HasBeenUpgraded = data.weapon1HasBeenUpgraded;
        weapon3HasBeenUpgraded = data.weapon1HasBeenUpgraded;
        weapon4HasBeenUpgraded = data.weapon1HasBeenUpgraded;

        ownsWeapon1 = data.ownsWeapon1;
        ownsWeapon2 = data.ownsWeapon2;
        ownsWeapon3 = data.ownsWeapon3;
        ownsWeapon4 = data.ownsWeapon4;

        savedWeightAmount1 = data.amountOfWeight1;
        savedWeightAmount2 = data.amountOfWeight2;
        savedWeightAmount3 = data.amountOfWeight3;
        savedWeightAmount4 = data.amountOfWeight4;

        savedSpeedAmount1 = data.amountOfSpeed1;
        savedSpeedAmount2 = data.amountOfSpeed2;
        savedSpeedAmount3 = data.amountOfSpeed3;
        savedSpeedAmount4 = data.amountOfSpeed4;
    }
   
    public void SetChosenWeaponID(int id) { weaponID = id;}
}

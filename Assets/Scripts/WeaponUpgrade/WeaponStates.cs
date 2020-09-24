using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: A script that handles the ownership issues of this system.
public class WeaponStates: MonoBehaviour
{
    private int weaponID;

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
    }
   
    public void SetChosenWeaponID(int id) { weaponID = id;}

    public int ReturnChosenWeaponID() { return weaponID;}
}

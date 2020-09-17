using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: A script that handles the ownership issues of this system.
public class WeaponStates: MonoBehaviour
{
    private int weaponID;

    public bool weaponHasBeenUpgraded;

    public bool ownsWeapon1;
    public bool ownsWeapon2;
    public bool ownsWeapon3;
    public bool ownsWeapon4;

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

    public void SetChosenWeaponID(int id)
    {
        weaponID = id;
    }

    public int ReturnChosenWeaponID()
    {
        return weaponID;
    }
}

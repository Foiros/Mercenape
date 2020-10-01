using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: A script that handles the ownership issues of this system.
public class WeaponStates: MonoBehaviour
{
    [SerializeField]private int weaponID;

    [SerializeField] private List<bool> ownedWeaponsList;
    [SerializeField] private List<bool> upgradedWeaponsList;
    [SerializeField] private List<int> savedSpeedAmountsList;

    private bool ownsWeapon1, ownsWeapon2, ownsWeapon3, ownsWeapon4;

    private bool weapon1HasBeenUpgraded, weapon2HasBeenUpgraded, weapon3HasBeenUpgraded, weapon4HasBeenUpgraded;
    
    private int savedSpeedAmount1, savedSpeedAmount2, savedSpeedAmount3, savedSpeedAmount4;

    void Awake()
    {
        SetUpBoolLists();
        LoadWeaponData();
    }

    void SetUpBoolLists()
    {
        ownedWeaponsList = new List<bool>() { ownsWeapon1, ownsWeapon2, ownsWeapon3, ownsWeapon4 };
        upgradedWeaponsList = new List<bool>() { weapon1HasBeenUpgraded, weapon2HasBeenUpgraded, weapon3HasBeenUpgraded, weapon4HasBeenUpgraded };
        savedSpeedAmountsList = new List<int>() { savedSpeedAmount1, savedSpeedAmount2, savedSpeedAmount3, savedSpeedAmount4 };

        ownedWeaponsList[0] = true;
    }

    // Switch loop function for setting the ownership status of a weapon.
    public void WhatWeaponWasBought(int id)
    {
        int weaponID = id;

        switch (weaponID)
        {
            case 1:
                ownedWeaponsList[1] = true;
                break;

            case 2:
                ownedWeaponsList[2] = true;
                break;

            case 3:
                ownedWeaponsList[3] = true;
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
                upgradedWeaponsList[0] = true;
                break;

            case 1:
                upgradedWeaponsList[1] = true;
                break;

            case 2:
                upgradedWeaponsList[2] = true;
                break;

            case 3:
                upgradedWeaponsList[3] = true;
                break;
        }
    }

    // Function for loading necessary data.
    void LoadWeaponData()
    {
        WeaponsData data = SaveManager.LoadWeapons();

        weaponID = data.weaponID;

        ownedWeaponsList = data.ownedWeaponsList;
        upgradedWeaponsList = data.upgradedWeaponsList;
        savedSpeedAmountsList = data.savedSpeedAmountsList;
    }

    public void SetChosenWeaponID(int id) { weaponID = id; }
    public void SetSavedSpeeds (List<int> list){ savedSpeedAmountsList = list; }


    public int GetChosenWeaponID() { return weaponID; }
    public List<bool> GetOwnedWeapons() { return ownedWeaponsList; }
    public List<bool> GetUpgradedWeapons() { return upgradedWeaponsList; }
    public List<int> GetSavedSpeeds() { return savedSpeedAmountsList; }
}

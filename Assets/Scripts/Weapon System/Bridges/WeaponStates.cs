using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: A script that handles the ownership issues of this system and sets up the weapon for level scene. 
public class WeaponStates: MonoBehaviour
{
    private StatsCalculator calculator;

    private List<AbstractWeapon> weapons;
    [SerializeField]private List<bool> ownedWeaponsList, upgradedWeaponsList;
    [SerializeField]private List<int> savedSpeedAmountsList;

    private int weaponID;

    [SerializeField] private float speed, impactDamage, bleedDamage, bleedDuration;
    private int bleedTicks;

    void Awake()
    {
        calculator = GetComponent<StatsCalculator>();

        SetUpBoolLists();
        LoadWeaponData();
    }

    void Start()
    {
        SetUpWeapon();
    }

    void SetUpBoolLists()
    {
        ownedWeaponsList = new List<bool>(new bool[4]);
        upgradedWeaponsList = new List<bool>(new bool[4]);
        savedSpeedAmountsList = new List<int>(new int[4]);

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

    // This function sets up the weapon to be used in level scenes. 
    void SetUpWeapon()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        GameObject weaponModel = weaponsArray.GetWeaponModel();

        weaponModel.SetActive(true);

        calculator.SetRequestFromActualWeapon(true);
        calculator.CalculateStats();

        speed = calculator.GetSpeed();
        impactDamage = calculator.GetImpactDamage();
        bleedDamage = weaponsArray.GetBleedDamage();
        bleedDuration = weaponsArray.GetBleedDuration();
        bleedTicks = weaponsArray.GetBleedTicks();
    }

    // Function for loading necessary data.
    void LoadWeaponData()
    {
        WeaponsData data = SaveManager.LoadWeapons();

        weaponID = data.weaponID;
        
        if(data.ownedWeaponsList != null) { ownedWeaponsList = data.ownedWeaponsList; }
        if(data.upgradedWeaponsList != null) { upgradedWeaponsList = data.upgradedWeaponsList; }
        if(data.savedSpeedAmountsList != null) { savedSpeedAmountsList = data.savedSpeedAmountsList; }
    }

    // Set functions
    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
    public void SetChosenWeaponID(int id) { weaponID = id; }
    public void SetSavedSpeeds (List<int> list){ savedSpeedAmountsList = list; }


    // Get Functions
    public int GetChosenWeaponID() { return weaponID; }
    public List<bool> GetOwnedWeapons() { return ownedWeaponsList; }
    public List<bool> GetUpgradedWeapons() { return upgradedWeaponsList; }
    public List<int> GetSavedSpeeds() { return savedSpeedAmountsList; }
    public float GetWeaponSpeed() { return speed; }
    public float GetWeaponImpactDamage() { return impactDamage; }
    public float GetWeaponBleedDamage() { return bleedDamage; }
    public float GetBleedDuration() { return bleedDuration; }
    public int GetWeaponBleedTicks() { return bleedTicks; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 24.9.2020: Script for handling asset set ups shared by multiple scripts, like for example setting up the abstract weapons. 
public class AssetManager : MonoBehaviour
{
    // Scripts
    private BuyWeapons buyWeapons;
    private UseUpgrades useUpgrades;
    private WeaponStats weaponStats;
    private SetActualWeapon setActualWeapon;
    private ChooseWeapon chooseWeapon;
    private PlayerAttackTrigger playerAttack;

    // Abstract object lists
    private List<AbstractWeapon> weapons = new List<AbstractWeapon>();
    private List<AbstractUpgrades> upgrades = new List<AbstractUpgrades>();

    // Sprite lists
    private List<Sprite> weaponImages;
    private List<Sprite> chosenWeaponImages;
    private List<Sprite> upgradeImages;

    void Awake()
    {
        GetNecessaryScripts();
        SetUpSprites();
        SetUpWeaponsAndUpgrades();
        SetUpArraysForOtherScripts();
    }

    // Function for getting all the other scripts needed by this script. 
    void GetNecessaryScripts()
    {
        buyWeapons = GetComponent<BuyWeapons>();
        useUpgrades = GetComponent<UseUpgrades>();
        weaponStats = GetComponent<WeaponStats>();
        setActualWeapon = GetComponent<SetActualWeapon>();
        chooseWeapon = GetComponent<ChooseWeapon>();

        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackTrigger>();
    }

    // Function for setting up images from the editor menu.
    void SetUpSprites()
    {
        Sprite[] weapons = Resources.LoadAll<Sprite>("Weapons");
        Sprite[] chosenWeapons = Resources.LoadAll<Sprite>("ChosenWeapons");
        Sprite[] upgrades = Resources.LoadAll<Sprite>("Upgrades");

        weaponImages = new List<Sprite>(weapons);
        chosenWeaponImages = new List<Sprite>(chosenWeapons);
        upgradeImages = new List<Sprite>(upgrades);
    }

    // Constructs weapons and upgrades and adds them to their own lists. 
    void SetUpWeaponsAndUpgrades()
    {
        weapons.Add(new TestWeapon("Weapon 1", "Does things", 0, 50, 5, 10, 20, 0.3f, 3f, weaponImages[0], chosenWeaponImages[0]));
        weapons.Add(new TestWeapon("Weapon 2", "Does things", 1, 25, 1, 20, 30, 0.3f, 2f, weaponImages[1], chosenWeaponImages[1]));
        weapons.Add(new TestWeapon("Weapon 3", "Does things", 2, 100, 3, 3, 10, 0.3f, 1f, weaponImages[2], chosenWeaponImages[2]));
        weapons.Add(new TestWeapon("Weapon 4", "Does things", 3, 150, 10, 2, 20, 0.3f, 5f, weaponImages[3], chosenWeaponImages[3]));

        upgrades.Add(new TestUpgrade("Speed Upgrade", "Increases the speed of your attacks", 0, 25, upgradeImages[0]));
        upgrades.Add(new TestUpgrade("Weigh Upgrade", "Increases the weight of your weapon", 1, 25, upgradeImages[1]));
    }

    // Basically this sends the weapons and upgrade lists to the scripts that use them. 
    void SetUpArraysForOtherScripts()
    {
        if (buyWeapons != null) { buyWeapons.SetWeaponList(weapons); }

        if (useUpgrades != null) { useUpgrades.SetWeaponList(weapons); useUpgrades.SetUpgradeList(upgrades); }

        if (weaponStats != null) { weaponStats.SetWeaponList(weapons); }

        if (chooseWeapon != null) { chooseWeapon.SetWeaponList(weapons); }

        if (setActualWeapon != null) { setActualWeapon.SetWeaponList(weapons); }

        if (playerAttack != null) { playerAttack.SetWeaponList(weapons); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 24.9.2020: Script for handling asset set ups shared by multiple scripts, like for example setting up the abstract weapons. 
public class AssetManager : MonoBehaviour
{
    // Scripts
    private WeaponStates weaponStates;
    private BuyWeapons buyWeapons;
    private UseUpgrades useUpgrades;
    private StatsCalculator calculator;
    private ChooseWeapon chooseWeapon;
    private PlayerAttackTrigger playerAttack;

    // Abstract object lists
    private List<AbstractWeapon> weapons = new List<AbstractWeapon>();
    private List<AbstractUpgrades> upgrades = new List<AbstractUpgrades>();

    // Sprite lists
    private List<Sprite> weaponImages, chosenWeaponImages, upgradeImages;

    // MeshRenrerer lists
    private List<GameObject> weaponModels;

    void Awake()
    {
        GetNecessaryScripts();
        SetUpSprites();
        SetUpModels();
        SetUpWeaponsAndUpgrades();
        SetUpArraysForOtherScripts();
    }

    // Function for getting all the other scripts needed by this script. 
    void GetNecessaryScripts()
    {
        weaponStates = GetComponent<WeaponStates>();
        buyWeapons = GetComponent<BuyWeapons>();
        useUpgrades = GetComponent<UseUpgrades>();
        calculator = GetComponent<StatsCalculator>();
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

    public void SetUpModels()
    {
        weaponModels = new List<GameObject>(GameObject.FindGameObjectsWithTag("WeaponInUse"));

        for (int i = 0; i < weaponModels.Count; i++)
        {
            weaponModels[i].SetActive(false);
        }
    }

    // Constructs weapons and upgrades and adds them to their own lists. 
    void SetUpWeaponsAndUpgrades()
    {
        weapons.Add(new TestWeapon("Weapon 1", "Does things", 0, 0, 2.0f, 1, 10, 1.5f, 3.0f, weaponImages[0], chosenWeaponImages[0], weaponModels[0]));
        weapons.Add(new TestWeapon("Weapon 2", "Does things", 1, 25, 1.0f, 7, 15, 5f, 3.0f, weaponImages[1], chosenWeaponImages[1], weaponModels[1]));
        weapons.Add(new TestWeapon("Weapon 3", "Does things", 2, 100, 1.5f, 3, 13, 7f, 3.0f, weaponImages[2], chosenWeaponImages[2], weaponModels[2]));
        weapons.Add(new TestWeapon("Weapon 4", "Does things", 3, 150, 2.5f, 1, 5, 10f, 3.0f, weaponImages[3], chosenWeaponImages[3], weaponModels[3]));

        upgrades.Add(new TestUpgrade("Speed Upgrade", "Increases the speed of your attacks", 0, 25, upgradeImages[0]));
    }

    // Basically this sends the weapons and upgrade lists to the scripts that use them. 
    void SetUpArraysForOtherScripts()
    {
        if(weaponStates != null) { weaponStates.SetWeaponList(weapons); }

        if (buyWeapons != null) { buyWeapons.SetWeaponList(weapons); }

        if (useUpgrades != null) { useUpgrades.SetWeaponList(weapons); useUpgrades.SetUpgradeList(upgrades); }

        if (calculator != null) { calculator.SetWeaponList(weapons); }

        if (chooseWeapon != null) { chooseWeapon.SetWeaponList(weapons); }

        if (playerAttack != null) { playerAttack.SetWeaponList(weapons); }
    }
}

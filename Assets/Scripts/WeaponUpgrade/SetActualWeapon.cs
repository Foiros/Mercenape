using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 16.9.2020: Script that set ups the weapon in game screen.
public class SetActualWeapon : MonoBehaviour
{
    private WeaponStates weaponStates;
    private WeaponStats weaponStats;
    private AssetManager assetManager;

    private AbstractWeapon[] weapons;

    private SpriteRenderer weaponInUseImage;

    private int weaponID;
    
    [SerializeField] private int speed;
    [SerializeField] private int weight;
    [SerializeField] private int impactDamage;
    
    void Awake()
    {
        weaponStates = GetComponent<WeaponStates>();
        weaponStats = GetComponent<WeaponStats>();
        assetManager = GetComponent<AssetManager>();

        weaponInUseImage = GameObject.FindGameObjectWithTag("WeaponInUse").GetComponent<SpriteRenderer>();
       
        weaponID = weaponStates.weaponID;

        SetUpWeaponsArray();
        SetUpWeapon();
    }

    // As in BuyWeapons script, this function sets up the abstract objects array, which can then be used by the code.
    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, 30, assetManager.weaponImages[0], null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, 20, assetManager.weaponImages[0], null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, 10, assetManager.weaponImages[0], null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, 20, assetManager.weaponImages[0], null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Sets up the stats and the image of the object.
    void SetUpWeapon()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        weaponInUseImage.sprite = weaponsArray.GetWeaponImage();

        weaponStats.SetRequestFromActualWeapon(true);
        weaponStats.CalculateStats();

        speed = weaponStats.GetSpeed();
        weight = weaponStats.GetWeight();
        impactDamage = weaponStats.GetImpactDamage();
    }

    public int GetChosenID() { return weaponID; }
    public int GetWeaponSpeed() { return speed; }
    public int GetWeaponImpactDamage() { return impactDamage; }
}

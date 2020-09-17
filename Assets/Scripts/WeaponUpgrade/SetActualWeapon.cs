using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 16.9.2020: Script that set ups the weapon in game screen.
public class SetActualWeapon : MonoBehaviour
{
    private AbstractWeapon[] weapons;
    private AbstractUpgrades[] upgrades;

    private Image weaponInUseImage;
    public  Sprite[] possibleWeaponsGallery;

    private int weaponID;
    private int upgradeID;
    
    private int speed;
    private int speedMult;
    private int actualSpeed;
    
    private int weight;
    private int weightMult;
    private int actualWeight;

    private bool weaponHasBeenUpgraded;
    
    void awake()
    {
        SetUpWeaponsArray();
        SetUpUpgradesArray();
        SetUpWeapon();
    }

    // As in BuyWeapons script, this function sets up the abstract objects array, which can then be used by the code.
    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, null, null, possibleWeaponsGallery[0]);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, null, null, possibleWeaponsGallery[1]);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, null, null, possibleWeaponsGallery[2]);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, null, null, possibleWeaponsGallery[3]);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Function to construct the upgrades and put them into an array.
    void SetUpUpgradesArray()
    {
        TestUpgrade1 testUpgrade1 = new TestUpgrade1("Speed Upgrade", "Increases the speed of your attacks", 0, 2, 0, null);
        TestUpgrade2 testUpgrade2 = new TestUpgrade2("Weigh Upgrade", "Increases the weight of your weapon", 1, 0, 2, null);

        upgrades = new AbstractUpgrades[] {testUpgrade1, testUpgrade2 };
    }

    // Sets up the stats and the image of the object.
    void SetUpWeapon()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];
        AbstractUpgrades upgradesArray = upgrades[upgradeID];

        weaponID = 0;

        weaponInUseImage.sprite = weaponsArray.GetInUseImage();

        speed = weaponsArray.GetSpeed(); 
        speedMult = upgradesArray.GetUpgradedSpeed();
        
        weight = weaponsArray.GetWeight();
        weightMult = upgradesArray.GetUpgradedWeight();

        if (weaponHasBeenUpgraded)
        {
            actualSpeed = speed * speedMult;
            actualWeight = weight * weightMult;
        }
        else
        {
            actualSpeed = speed;
            actualWeight = weight;
        }
        
    }
}

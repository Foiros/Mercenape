using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Created by Arttu Paldán 16.9.2020: This script allows the player to place upgrades into his weapon. 
public class UseUpgrades : MonoBehaviour
{
    private AbstractWeapon[] weapons;
    private AbstractUpgrades[] upgrades;

    private int weaponID;
    private int upgradeID;

    private string weaponButtonName;
    private string upgradeButtonName;

    public GameObject upgradeMenu;
    public GameObject upgradeComponentScreen;

    public Sprite[] upgradeImages;
    public Sprite[] ownedWeapons;

    private Text weaponName;
    private Text weaponDescription;
    private Image weaponImage;

    private Text upgradeName;
    private Text upgradeDescription;
    private Image upgradeImage;


    void Awake()
    {
        upgradeMenu.SetActive(false);
        upgradeComponentScreen.SetActive(false);

        SetUpWeaponsArray();
        SetUpUpgradesArray();
    }
     void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, null, null, ownedWeapons[0]);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, null, null, ownedWeapons[1]);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, null, null, ownedWeapons[3]);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, null, null, ownedWeapons[4]);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Function sets up the upgrades array for this script to use.
    void SetUpUpgradesArray()
    {
        TestUpgrade1 testUpgrade1 = new TestUpgrade1("Speed Upgrade", "Increases the speed of your attacks", 0, 2, 0, upgradeImages[0]);
        TestUpgrade2 testUpgrade2 = new TestUpgrade2("Weigh Upgrade", "Increases the weight of your weapon", 1, 0, 2, upgradeImages[1]);

        upgrades = new AbstractUpgrades[] { testUpgrade1, testUpgrade2 };
    }

    // Button function, that detects which button has been pressed and returns ID based on that. 
    public void OpenUpgradeMenu()
    {
        weaponButtonName = EventSystem.current.currentSelectedGameObject.name;

        if(weaponButtonName == "OwnedWeaponA")
        {
            weaponID = weapons[0].GetID();
        }
        else if (weaponButtonName == "OwnedWeaponB")
        {
            weaponID = weapons[1].GetID();
        }
        else if (weaponButtonName == "OwnedWeaponC")
        {
            weaponID = weapons[2].GetID();
        }
        else if (weaponButtonName == "OwnedWeaponD")
        {
            weaponID = weapons[3].GetID();
        }

        SetUpUpgradeScreen();
    }

    void SetUpUpgradeScreen()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        upgradeMenu.SetActive(true);

        weaponName.text = weaponsArray.GetName();
        weaponDescription.text = weaponsArray.GetDescription();
        weaponImage.sprite = weaponsArray.GetInUseImage();
    }

    public void OpenUpgradeComponent()
    {
        upgradeButtonName = EventSystem.current.currentSelectedGameObject.name;

        if (upgradeButtonName == "UpgradeButton1")
        {
            upgradeID = upgrades[0].GetID();
        }
        else if (upgradeButtonName == "UpgradeButton2")
        {
            upgradeID= upgrades[1].GetID();
        }
        else if (upgradeButtonName == "UpgradeButton3")
        {
            upgradeID = upgrades[2].GetID();
        }
        else if (upgradeButtonName == "UpgradeButton4")
        {
            upgradeID = upgrades[3].GetID();
        }

        SetUpUpgradeComponentScreen();
    }

    void SetUpUpgradeComponentScreen()
    {
        AbstractUpgrades upgradesArray = upgrades[upgradeID];

        upgradeComponentScreen.SetActive(true);

        upgradeName.text = upgradesArray.GetName();
        upgradeDescription.text = upgradesArray.GetDescription();
        upgradeImage.sprite = upgradesArray.GetUpgradeImage();
    }

    void ChooseAmount()
    {

    }

    void ConfirmUpgrade()
    {

    }
}

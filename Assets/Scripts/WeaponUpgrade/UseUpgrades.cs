using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Created by Arttu Paldán 16.9.2020: This script allows the player to place upgrades into his weapon. 
public class UseUpgrades : MonoBehaviour
{
    private WeaponStates weaponStates;
    
    private AbstractWeapon[] weapons;
    private AbstractUpgrades[] upgrades;

    private int weaponID;
    private int upgradeID;

    private string weaponButtonName;
    private string upgradeButtonName;

    public GameObject upgradeMenu;
    public GameObject upgradeComponentScreen;

    public Image[] upgradeImagesHolder;
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
        weaponStates = GetComponent<WeaponStates>();
        
        weaponName = GameObject.FindGameObjectWithTag("UpgradeScreenWeaponName").GetComponent<Text>();
        weaponDescription = GameObject.FindGameObjectWithTag("UpgradeScreenWeaponDescription").GetComponent<Text>();
        weaponImage = GameObject.FindGameObjectWithTag("UpgradeScreenWeaponImage").GetComponent<Image>();

        upgradeName = GameObject.FindGameObjectWithTag("UpgradeName").GetComponent<Text>();
        upgradeDescription = GameObject.FindGameObjectWithTag("UpgradeDescription").GetComponent<Text>();
        upgradeImage = GameObject.FindGameObjectWithTag("UpgradeImage").GetComponent<Image>();

        upgradeMenu.SetActive(false);
        upgradeComponentScreen.SetActive(false);

        SetUpWeaponsArray();
        SetUpUpgradesArray();
    }
    
    // Function for setting up the abstract weapons in this script and putting them into an array. 
     void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, null, null, ownedWeapons[0]);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, null, null, ownedWeapons[1]);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, null, null, ownedWeapons[2]);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, null, null, ownedWeapons[3]);

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

        if(weaponButtonName == "UpgradeButton1" && weaponStates.ownsWeapon1)
        {
            weaponID = weapons[0].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton2" && weaponStates.ownsWeapon2)
        {
            weaponID = weapons[1].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton3" && weaponStates.ownsWeapon3)
        {
            weaponID = weapons[2].GetID();
            SetUpUpgradeScreen();
        }
        else if (weaponButtonName == "UpgradeButton4" && weaponStates.ownsWeapon4)
        {
            weaponID = weapons[3].GetID();
            SetUpUpgradeScreen();
        }
    }
    
    // This function sets up the upgrade screen based on what weapon player has chosen from his owned weapons inventory. 
    void SetUpUpgradeScreen()
    {
        AbstractWeapon weaponsArray = weapons[weaponID];

        upgradeMenu.SetActive(true);

        weaponName.text = weaponsArray.GetName();
        weaponDescription.text = weaponsArray.GetDescription();
        weaponImage.sprite = weaponsArray.GetInUseImage();
        
        for(int i = 0; i < upgradeImagesHolder.Length; i++)
        {
            upgradeImagesHolder[i].sprite = upgrades[i].GetUpgradeImage();
        }
    }

    // Button function for opening an screen that will explain, what the upgrade does when put into the weapon. 
    public void OpenUpgradeComponent()
    {
        upgradeButtonName = EventSystem.current.currentSelectedGameObject.name;

        if (upgradeButtonName == "UpgradeComponent1")
        {
            upgradeID = upgrades[0].GetID();
        }
        else if (upgradeButtonName == "UpgradeComponent2")
        {
            upgradeID= upgrades[1].GetID();
        }
        else if (upgradeButtonName == "UpgradeComponent3")
        {
            upgradeID = upgrades[2].GetID();
        }
        
        SetUpUpgradeComponentScreen();
    }

    // Sets up the upgrade explanation screen. 
    void SetUpUpgradeComponentScreen()
    {
        AbstractUpgrades upgradesArray = upgrades[upgradeID];

        upgradeComponentScreen.SetActive(true);

        upgradeName.text = upgradesArray.GetName();
        upgradeDescription.text = upgradesArray.GetDescription();
        upgradeImage.sprite = upgradesArray.GetUpgradeImage();
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        weaponID = -1;
    }

    public void CloseUpgradeComponentMenu()
    {
        upgradeComponentScreen.SetActive(false);
        upgradeID = -1;
    }

    public void ChooseAmount()
    {

    }

    public void ConfirmUpgrade()
    {
        weaponStates.weaponHasBeenUpgraded = true;
    }
}

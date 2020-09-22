using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Created by Arttu Paldán 16.9.2020: This script allows the player to place upgrades into his weapon. 
public class UseUpgrades : MonoBehaviour
{
    private WeaponStates weaponStates;
    private AssetManager assetManager;
    
    private AbstractWeapon[] weapons;
    private AbstractUpgrades[] upgrades;

    private int weaponID;
    private int upgradeID;
    private int tempUpgradeID1;
    private int tempUpgradeID2;
    private int tempUpgradeID3;

    private string weaponButtonName;
    private string upgradeButtonName;
    private string scrollButtonName;

    public Image[] upgradeImagesHolder;
    public GameObject upgradeMenu;
    public GameObject upgradeComponentScreen;

    private Text weaponName;
    private Text weaponDescription;
    private Image weaponImage;
   
    private Text upgradeName;
    private Text upgradeDescription;
    private Image upgradeImage;


    void Awake()
    {
        weaponStates = GetComponent<WeaponStates>();
        assetManager = GetComponent<AssetManager>();
        
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
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, assetManager.weaponImages[0], null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, assetManager.weaponImages[0], null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, assetManager.weaponImages[0], null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, assetManager.weaponImages[0], null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Function sets up the upgrades array for this script to use.
    void SetUpUpgradesArray()
    {
        TestUpgrade1 testUpgrade1 = new TestUpgrade1("Speed Upgrade", "Increases the speed of your attacks", 0, 2, 0, assetManager.upgradeImages[0]);
        TestUpgrade2 testUpgrade2 = new TestUpgrade2("Weigh Upgrade", "Increases the weight of your weapon", 1, 0, 2, assetManager.upgradeImages[1]);

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
        weaponImage.sprite = weaponsArray.GetWeaponImage();
        
        for(int i = 0; i < upgradeImagesHolder.Length; i++)
        {
            upgradeImagesHolder[i].sprite = upgrades[i].GetUpgradeImage();
        }

        tempUpgradeID1 = upgrades[0].GetID();
        tempUpgradeID2 = upgrades[1].GetID();
        tempUpgradeID3 = upgrades[2].GetID();
    }

    // Button function for opening an screen that will explain, what the upgrade does when put into the weapon. 
    public void OpenUpgradeComponent()
    {
        upgradeButtonName = EventSystem.current.currentSelectedGameObject.name;

        if (upgradeButtonName == "UpgradeComponent1")
        {
            upgradeID = upgrades[tempUpgradeID1].GetID();
        }
        else if (upgradeButtonName == "UpgradeComponent2")
        {
            upgradeID = upgrades[tempUpgradeID2].GetID();
        }
        else if (upgradeButtonName == "UpgradeComponent3")
        {
            upgradeID = upgrades[tempUpgradeID3].GetID();
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

    public void ScrollUpgrades()
    {
        scrollButtonName = EventSystem.current.currentSelectedGameObject.name;

        if(scrollButtonName == "NextButton1")
        {
            if (tempUpgradeID1 < upgrades.Length)
            {
                tempUpgradeID1 = tempUpgradeID1 + 1;
            }
            else if (tempUpgradeID1 >= upgrades.Length)
            {
                tempUpgradeID1 = tempUpgradeID1 - upgrades.Length;
            }
            
            upgradeImagesHolder[0].sprite = upgrades[tempUpgradeID1].GetUpgradeImage();
        }
        else if (scrollButtonName == "NextButton2")
        {
            if (tempUpgradeID2 < upgrades.Length)
            {
                tempUpgradeID2 = tempUpgradeID2 + 1;
            }
            else if (tempUpgradeID2 >= upgrades.Length)
            {
                tempUpgradeID2 = tempUpgradeID2 - upgrades.Length;
            }

            upgradeImagesHolder[1].sprite = upgrades[tempUpgradeID2].GetUpgradeImage();
        }
        else if (scrollButtonName == "NextButton3")
        {
            if (tempUpgradeID3 < upgrades.Length)
            {
                tempUpgradeID3 = tempUpgradeID3 + 1;
            }
            else if (tempUpgradeID1 >= upgrades.Length)
            {
                tempUpgradeID3 = tempUpgradeID3 - upgrades.Length;
            }

            upgradeImagesHolder[2].sprite = upgrades[tempUpgradeID3].GetUpgradeImage();
        }
    }


    public void ConfirmUpgrade()
    {
        weaponStates.weaponHasBeenUpgraded = true;
    }
}

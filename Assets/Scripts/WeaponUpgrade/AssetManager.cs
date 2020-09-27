using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private BuyWeapons buyWeapons;
    private UseUpgrades useUpgrades;
    private WeaponStats weaponStats;
    private SetActualWeapon setActualWeapon;
    private ChooseWeapon chooseWeapon;
    private PlayerAttackTrigger playerAttack;

    private List<AbstractWeapon> weapons = new List<AbstractWeapon>();
    private List<AbstractUpgrades> upgrades = new List<AbstractUpgrades>();

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

    void GetNecessaryScripts()
    {
        buyWeapons = GetComponent<BuyWeapons>();
        useUpgrades = GetComponent<UseUpgrades>();
        weaponStats = GetComponent<WeaponStats>();
        setActualWeapon = GetComponent<SetActualWeapon>();
        chooseWeapon = GetComponent<ChooseWeapon>();

        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackTrigger>();
    }
    void SetUpSprites()
    {
        Sprite[] weapons = Resources.LoadAll<Sprite>("Weapons");
        Sprite[] chosenWeapons = Resources.LoadAll<Sprite>("ChosenWeapons");
        Sprite[] upgrades = Resources.LoadAll<Sprite>("Upgrades");

        weaponImages = new List<Sprite>(weapons);
        chosenWeaponImages = new List<Sprite>(chosenWeapons);
        upgradeImages = new List<Sprite>(upgrades);
    }

    void SetUpWeaponsAndUpgrades()
    {
        weapons.Add(new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, 20, 0.3f, 3f, weaponImages[0], chosenWeaponImages[0]));
        weapons.Add(new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, 30, 0.3f, 2f, weaponImages[1], chosenWeaponImages[1]));
        weapons.Add(new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, 10, 0.3f, 1f, weaponImages[2], chosenWeaponImages[2]));
        weapons.Add(new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, 20, 0.3f, 5f, weaponImages[3], chosenWeaponImages[3]));

        upgrades.Add(new TestUpgrade1("Speed Upgrade", "Increases the speed of your attacks", 0, 25, upgradeImages[0]));
        upgrades.Add(new TestUpgrade2("Weigh Upgrade", "Increases the weight of your weapon", 1, 25, upgradeImages[1]));
    }

    void SetUpArraysForOtherScripts()
    {
        if(buyWeapons != null) { buyWeapons.SetWeaponList(weapons);}
        
        if(useUpgrades != null) { useUpgrades.SetWeaponList(weapons); useUpgrades.SetUpgradeList(upgrades); }

        if(weaponStats != null) { weaponStats.SetWeaponList(weapons); }

        if(chooseWeapon != null) { chooseWeapon.SetWeaponList(weapons); }
        
        if(setActualWeapon != null) { setActualWeapon.SetWeaponList(weapons);}

        if(playerAttack != null) { playerAttack.SetWeaponList(weapons); }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: Static class that handles the saving of weapon related data. 
public static class SaveManager
{
    // Function for saving the data we want to save. It uses the binary formatter to save data. 
    public static void SaveWeapons(WeaponStates weaponStates)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string weaponsPath = Application.persistentDataPath + "/Weapons.data";
        FileStream stream = new FileStream(weaponsPath, FileMode.Create);

        WeaponsData weaponsData = new WeaponsData(weaponStates);

        formatter.Serialize(stream, weaponsData);
        stream.Close();
    }

    // Load function.
    public static WeaponsData LoadWeapons()
    {
        string weaponsPath = Application.persistentDataPath + "/Weapons.data";
        if (File.Exists(weaponsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(weaponsPath, FileMode.Open);

            WeaponsData weaponsData = formatter.Deserialize(stream) as WeaponsData;
            stream.Close();

            return weaponsData;
        }
        else
        {
            return null;
        }
    }

    // Delete data function. 
    public static void DeleteWeapons()
    {
        string weaponsPath = Application.persistentDataPath + "/Weapons.data";
        File.Delete(weaponsPath);
    }

    // Function for saving the data we want to save. It uses the binary formatter to save data. 
    public static void SaveCurrency(PlayerCurrency playerCurrency)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string currencyPath = Application.persistentDataPath + "/Currency.data";
        FileStream stream = new FileStream(currencyPath, FileMode.Create);

        CurrencyData currencyData = new CurrencyData(playerCurrency);

        formatter.Serialize(stream, currencyData);
        stream.Close();
    }

    // Load function.
    public static CurrencyData LoadCurrency()
    {
        string currencyPath = Application.persistentDataPath + "/Currency.data";
        if (File.Exists(currencyPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(currencyPath, FileMode.Open);

            CurrencyData currencyData = formatter.Deserialize(stream) as CurrencyData;
            stream.Close();

            return currencyData;
        }
        else
        {
            return null;
        }
    }

    // Delete data function. 
    public static void DeleteCurrency()
    {
        string currencyPath = Application.persistentDataPath + "/Currency.data";
        File.Delete(currencyPath);
    }
}

// Save data class, which contains the infor we want to save. 
[System.Serializable]
public class WeaponsData
{
    public int weaponID;

    public List<bool> ownedWeaponsList;
    public List<bool> upgradedWeaponsList;
    public List<int> savedWeightAmountsList;
    public List<int> savedSpeedAmountsList;

    public bool ownsWeapon1;
    public bool ownsWeapon2;
    public bool ownsWeapon3;
    public bool ownsWeapon4;

    public bool weapon1HasBeenUpgraded;
    public bool weapon2HasBeenUpgraded;
    public bool weapon3HasBeenUpgraded;
    public bool weapon4HasBeenUpgraded;

    public int amountOfWeight1;
    public int amountOfWeight2;
    public int amountOfWeight3;
    public int amountOfWeight4;

    public int amountOfSpeed1;
    public int amountOfSpeed2;
    public int amountOfSpeed3;
    public int amountOfSpeed4;

    public WeaponsData(WeaponStates weaponStates)
    {
        weaponID = weaponStates.GetChosenWeaponID();

        ownedWeaponsList = weaponStates.GetOwnedWeapons();
        upgradedWeaponsList = weaponStates.GetUpgradedWeapons();
        savedWeightAmountsList = weaponStates.GetSavedWeights();
        savedSpeedAmountsList = weaponStates.GetSavedSpeeds();
    }
}

[System.Serializable]
public class CurrencyData
{
    public int playerMoney;
    public int playerKarma;
    public int speedUpgrades;

    public CurrencyData(PlayerCurrency playerCurrency)
    {
        playerMoney = playerCurrency.playerGold;
        playerKarma = playerCurrency.playerKarma;
        speedUpgrades = playerCurrency.playerUpgrade;
    }
}
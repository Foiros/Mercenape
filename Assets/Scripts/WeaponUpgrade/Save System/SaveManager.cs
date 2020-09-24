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
}

// Save data class, which contains the infor we want to save. 
[System.Serializable]
public class WeaponsData
{
    public int weaponID;

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
        weaponID = weaponStates.weaponID;

        weapon1HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;
        weapon2HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;
        weapon3HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;
        weapon4HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;

        ownsWeapon1 = weaponStates.ownsWeapon1;
        ownsWeapon2 = weaponStates.ownsWeapon2;
        ownsWeapon3 = weaponStates.ownsWeapon3;
        ownsWeapon4 = weaponStates.ownsWeapon4;

        amountOfWeight1 = weaponStates.savedWeightAmount1;
        amountOfWeight2 = weaponStates.savedWeightAmount2;
        amountOfWeight3 = weaponStates.savedWeightAmount3;
        amountOfWeight4 = weaponStates.savedWeightAmount4;

        amountOfSpeed1 = weaponStates.savedSpeedAmount1;
        amountOfSpeed2 = weaponStates.savedSpeedAmount2;
        amountOfSpeed3 = weaponStates.savedSpeedAmount3;
        amountOfSpeed4 = weaponStates.savedSpeedAmount4;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Created by Arttu Paldán 17.9.2020: 
public static class SaveManager
{
    // Function for saving the data we want to save. It uses the binary formatter to save data. 
    public static void SaveWeapons(WeaponStates weaponStates, WeaponStats weaponStats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string weaponsPath = Application.persistentDataPath + "/Weapons.data";
        FileStream stream = new FileStream(weaponsPath, FileMode.Create);

        WeaponsData weaponsData = new WeaponsData(weaponStates, weaponStats);

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

    public bool weapon1HasBeenUpgraded;
    public bool weapon2HasBeenUpgraded;
    public bool weapon3HasBeenUpgraded;
    public bool weapon4HasBeenUpgraded;

    public bool ownsWeapon1;
    public bool ownsWeapon2;
    public bool ownsWeapon3;
    public bool ownsWeapon4;

    public int amountOfWeight1;
    public int amountOfWeight2;
    public int amountOfWeight3;
    public int amountOfWeight4;

    public int amountOfSpeed1;
    public int amountOfSpeed2;
    public int amountOfSpeed3;
    public int amountOfSpeed4;

    public WeaponsData(WeaponStates weaponStates, WeaponStats weaponStats)
    {
        weaponID = weaponStates.ReturnChosenWeaponID();

        weapon1HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;
        weapon2HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;
        weapon3HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;
        weapon4HasBeenUpgraded = weaponStates.weapon1HasBeenUpgraded;

        ownsWeapon1 = weaponStates.ownsWeapon1;
        ownsWeapon2 = weaponStates.ownsWeapon2;
        ownsWeapon3 = weaponStates.ownsWeapon3;
        ownsWeapon4 = weaponStates.ownsWeapon4;

        amountOfWeight1 = weaponStats.amountOfWeight;
        
        amountOfSpeed1 = weaponStats.amountOfSpeed;
    }
}
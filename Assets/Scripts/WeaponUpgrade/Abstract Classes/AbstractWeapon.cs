using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 11.9.2020: An abstract used to create weapon components. 
public abstract class AbstractWeapon
{ 
    // Weapon details
    public string weaponName;
    public string weaponDescription;

    public int weaponID;
    public int weaponCost;

    public Sprite weaponImage;
    public Sprite chosenWeaponImage;

    // Weapon stats
    public int weaponSpeed;
    public int weaponWeight;


    // Fetch functions for details
    public string GetName()
    {
        return weaponName;
    }
    public string GetDescription()
    {
        return weaponDescription;
    }

    public int GetID()
    {
        return weaponID;
    }

    public int GetCost()
    {
        return weaponCost;
    }

    public Sprite GetWeaponImage()
    {
        return weaponImage;
    }

    public Sprite GetChosenWeaponImage()
    {
        return chosenWeaponImage;
    }

    // Fetch functios for stats
    public int GetSpeed()
    {
        return weaponSpeed;
    }

    public int GetWeight()
    {
        return weaponWeight;
    }

    // Special abilities of the weapons
    public abstract void WeaponEffect();
}

public class TestWeapon1 : AbstractWeapon
{   
    // This is used to create this object in the scripts. 
    public TestWeapon1(string name, string description, int id, int cost, int speed, int weight, Sprite weapon, Sprite chosenWeapon)
    {
        weaponName = name;
        weaponDescription = description;
        weaponID = id;
        weaponCost = cost;
        weaponSpeed = speed;
        weaponWeight = weight;
        weaponImage = weapon;
        chosenWeaponImage = chosenWeapon;
    }

    // Eventually this will hold the effect the component has on players weapon.
    public override void WeaponEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class TestWeapon2 : AbstractWeapon
{
    public TestWeapon2(string name, string description, int id, int cost, int speed, int weight, Sprite weapon, Sprite chosenWeapon)
    {
        weaponName = name;
        weaponDescription = description;
        weaponID = id;
        weaponCost = cost;
        weaponSpeed = speed;
        weaponWeight = weight;
        weaponImage = weapon;
        chosenWeaponImage = chosenWeapon;
    }
    
    public override void WeaponEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class TestWeapon3 : AbstractWeapon
{
    public TestWeapon3(string name, string description, int id, int cost, int speed, int weight, Sprite weapon, Sprite chosenWeapon)
    {
        weaponName = name;
        weaponDescription = description;
        weaponID = id;
        weaponCost = cost;
        weaponSpeed = speed;
        weaponWeight = weight;
        weaponImage = weapon;
        chosenWeaponImage = chosenWeapon;
    }
    
    public override void WeaponEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class TestWeapon4 : AbstractWeapon
{
    public TestWeapon4(string name, string description, int id, int cost, int speed, int weight, Sprite weapon, Sprite chosenWeapon)
    {
        weaponName = name;
        weaponDescription = description;
        weaponID = id;
        weaponCost = cost;
        weaponSpeed = speed;
        weaponWeight = weight;
        weaponImage = weapon;
        chosenWeaponImage = chosenWeapon;
    }
   
    public override void WeaponEffect()
    {
        throw new System.NotImplementedException();
    }
}

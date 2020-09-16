using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 16.9.2020: An abstract class for creating weapon upgrades. 
public abstract class AbstractUpgrades
{
    public string upgradeName;
    public string upgradeDescription;

    public int upgradeID;
    public int upgradeAmount;
    public int upgradedSpeedMult;
    public int UpgradedWeightMult;

    // Fetch functions for the variables.
    public string GetName()
    {
        return upgradeName;
    }

    public string GetDescription()
    {
        return upgradeDescription;
    }

    public int GetID()
    {
        return upgradeID;
    }

    public int GetAmount()
    {
        return upgradeAmount;
    }

    public int GetUpgradedSpeed()
    {
        return upgradedSpeedMult;
    }

    public int GetUpgradedWeight()
    {
        return UpgradedWeightMult;
    }
}

public class TestUpgrade1 : AbstractUpgrades
{
    //Constructor for this particular upgradde. 
    public TestUpgrade1(string name, string description, int id, int amount, int speed, int weight)
    {
        upgradeName = name;
        upgradeDescription = description;
        upgradeID = id;
        upgradeAmount = amount;
        upgradedSpeedMult = speed;
        UpgradedWeightMult = weight;
    }
}

public class TestUpgrade2 : AbstractUpgrades
{
    public TestUpgrade2(string name, string description, int id, int amount, int speed, int weight)
    {
        upgradeName = name;
        upgradeDescription = description;
        upgradeID = id;
        upgradeAmount = amount;
        upgradedSpeedMult = speed;
        UpgradedWeightMult = weight;
    }
}

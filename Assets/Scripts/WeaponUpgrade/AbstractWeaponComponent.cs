using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 11.9.2020: An abstract used to create weapon components. 
public abstract class AbstractWeaponComponent
{ 
    public string componentName;
    public string componentDescription;

    public int compomnentID;

    public int componentCost;

    public Sprite componentNotBougthImage;

    public Sprite componentBoughtImage;

    public Sprite placedComponentImage;

    public string GetName()
    {
        return componentName;
    }
    public string GetDescription()
    {
        return componentDescription;
    }

    public int GetID()
    {
        return compomnentID;
    }

    public int GetCost()
    {
        return componentCost;
    }

    public Sprite GetNotBoughtImage()
    {
        return componentNotBougthImage;
    }
    public Sprite GetBoughtImage()
    {
        return componentBoughtImage;
    }

    public Sprite GetPlacedImage()
    {
        return placedComponentImage;
    }
    public abstract void ComponentEffect();
}

public class TestComponent1 : AbstractWeaponComponent
{   
    // This is used to create this object in the scripts. 
    public TestComponent1(string name, string description, int id, int cost, Sprite notBought, Sprite bought, Sprite placed)
    {
        componentName = name;
        componentDescription = description;
        compomnentID = id;
        componentCost = cost;
        componentNotBougthImage = notBought;
        componentBoughtImage = bought;
        placedComponentImage = placed;
    }

    // Eventually this will hold the effect the component has on players weapon.
    public override void ComponentEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class TestComponent2 : AbstractWeaponComponent
{
    public TestComponent2(string name, string description, int id, int cost, Sprite notBought, Sprite bought, Sprite placed)
    {
        componentName = name;
        componentDescription = description;
        compomnentID = id;
        componentCost = cost;
        componentNotBougthImage = notBought;
        componentBoughtImage = bought;
        placedComponentImage = placed;
    }
    public override void ComponentEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class TestComponent3 : AbstractWeaponComponent
{
    public TestComponent3(string name, string description, int id, int cost, Sprite notBought, Sprite bought, Sprite placed)
    {
        componentName = name;
        componentDescription = description;
        compomnentID = id;
        componentCost = cost;
        componentNotBougthImage = notBought;
        componentBoughtImage = bought;
        placedComponentImage = placed;
    }
    public override void ComponentEffect()
    {
        throw new System.NotImplementedException();
    }
}

public class TestComponent4 : AbstractWeaponComponent
{
    public TestComponent4(string name, string description, int id, int cost, Sprite notBought, Sprite bought, Sprite placed)
    {
        componentName = name;
        componentDescription = description;
        compomnentID = id;
        componentCost = cost;
        componentNotBougthImage = notBought;
        componentBoughtImage = bought;
        placedComponentImage = placed;
    }
    public override void ComponentEffect()
    {
        throw new System.NotImplementedException();
    }
}

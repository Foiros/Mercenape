using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 11.9.2020: An abstract used to create weapon components. 
public abstract class AbstractWeapon
{ 
    // Weapon details
    protected string weaponName;
    protected string weaponDescription;

    protected int weaponID;
    protected int weaponCost;

    protected Sprite weaponImage;
    protected Sprite chosenWeaponImage;

    // Weapon stats
    protected int weaponSpeed;
    protected int weaponWeight;
    protected int impactDamage;
    protected int bleedDamage;
    protected int bleedDuration;

    // Weapon reach and hit box
    protected float hitBox;
    protected float hitBoxLocation;

    
    // Fetch functions for details
    public string GetName() { return weaponName;}
    public string GetDescription() { return weaponDescription;}
    public int GetID() { return weaponID;}
    public int GetCost() { return weaponCost;}
    public Sprite GetWeaponImage(){ return weaponImage;}
    public Sprite GetChosenWeaponImage() { return chosenWeaponImage; }

    // Fetch functios for stats
    public int GetSpeed() { return weaponSpeed;}
    public int GetWeight() { return weaponWeight;}
    public int GetImpactDamage() { return impactDamage; }
    public int GetBleedDamage() { return bleedDamage; }
    public int GetBleedDuration() { return bleedDuration; }

    // Fetch functions for reach and hitbox
    public float GetHitBox() { return hitBox; }
    public float GetReach() { return hitBoxLocation; }
}

public class TestWeapon : AbstractWeapon
{
    // This is used to create this object in the scripts. 
    public TestWeapon(string name, string description, int id, int cost, int speed, int weight, int damage, float range, float location, Sprite weapon, Sprite chosenWeapon)
    {
        weaponName = name;
        weaponDescription = description;
        weaponID = id;
        weaponCost = cost;
        weaponSpeed = speed;
        weaponWeight = weight;
        impactDamage = damage;
        hitBox = range;
        hitBoxLocation = location;
        weaponImage = weapon;
        chosenWeaponImage = chosenWeapon;
    }
}

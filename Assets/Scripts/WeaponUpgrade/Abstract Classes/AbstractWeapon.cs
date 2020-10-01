using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 11.9.2020: An abstract used to create weapon components. 
public abstract class AbstractWeapon
{ 
    // Weapon details
    protected string weaponName, weaponDescription;

    // Weapon id and cost
    protected int weaponID, weaponCost;

    // Weapon images
    protected Sprite weaponImage, chosenWeaponImage;

    // Weapon stats
    protected float weaponSpeed, weaponWeight, impactDamage, bleedDamage, bleedDuration;

    // Weapon reach and hit box
    protected float hitBox, hitBoxLocation;


    // Fetch functions for details
    public string GetName() { return weaponName;}
    public string GetDescription() { return weaponDescription;}
    public int GetID() { return weaponID;}
    public int GetCost() { return weaponCost;}
    public Sprite GetWeaponImage(){ return weaponImage;}
    public Sprite GetChosenWeaponImage() { return chosenWeaponImage; }

    // Fetch functios for stats
    public float GetSpeed() { return weaponSpeed;}
    public float GetWeight() { return weaponWeight;}
    public float GetImpactDamage() { return impactDamage; }
    public float GetBleedDamage() { return bleedDamage; }
    public float GetBleedDuration() { return bleedDuration; }

    // Fetch functions for reach and hitbox
    public float GetHitBox() { return hitBox; }
    public float GetReach() { return hitBoxLocation; }
}

public class TestWeapon : AbstractWeapon
{
    // This is used to create this object in the scripts. 
    public TestWeapon(string name, string description, int id, int cost, float speed, float weight, float damage, float range, float location, Sprite weapon, Sprite chosenWeapon)
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

﻿using System.Collections;
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

    // Weapon model
    protected GameObject weaponModel;

    // Weapon stats
    protected float weaponSpeed, weaponWeight;
        
    protected int impactDamage, bleedDamage, bleedDuration;

    // Weapon reach and hit box
    protected Vector3 hitBoxLocation, hitBoxSize;


    // Fetch functions for details
    public string GetName() { return weaponName;}
    public string GetDescription() { return weaponDescription;}
    public int GetID() { return weaponID;}
    public int GetCost() { return weaponCost;}
    public Sprite GetWeaponImage(){ return weaponImage;}
    public Sprite GetChosenWeaponImage() { return chosenWeaponImage; }
    public GameObject GetWeaponModel() { return weaponModel; }

    // Fetch functios for stats
    public float GetSpeed() { return weaponSpeed;}
    public float GetWeight() { return weaponWeight;}
    public int GetImpactDamage() { return impactDamage; }
    public float GetBleedDamage() { return bleedDamage; }
    public float GetBleedDuration() { return bleedDuration; }

    // Fetch functions for reach and hitbox
    public Vector3 GetHitBoxSize() { return hitBoxSize; }
    public Vector3 GetHitBoxLocation() { return hitBoxLocation; }
}

public class TestWeapon : AbstractWeapon
{
    // This is used to create this object in the scripts. 
    public TestWeapon(string name, string description, int id, int cost, float speed, float weight, int damage, Vector3 location, Vector3 size, Sprite weapon, Sprite chosenWeapon, GameObject model)
    {
        weaponName = name;
        weaponDescription = description;
        weaponID = id;
        weaponCost = cost;
        weaponSpeed = speed;
        weaponWeight = weight;
        impactDamage = damage;
        hitBoxSize = size;
        hitBoxLocation = location;
        weaponImage = weapon;
        chosenWeaponImage = chosenWeapon;
        weaponModel = model;
    }
}

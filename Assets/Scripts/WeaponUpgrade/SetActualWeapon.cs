using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 16.9.2020: Script that set ups the weapon in game screen.
public class SetActualWeapon : MonoBehaviour
{
    private WeaponStates weaponStates;
    private WeaponStats weaponStats;

    private List<AbstractWeapon> weapons;

    private SpriteRenderer weaponInUseImage;

    [SerializeField]private int weaponID;
    
    [SerializeField] private int speed;
    [SerializeField] private int impactDamage;
    
    void Awake()
    {
        weaponStates = GetComponent<WeaponStates>();
        weaponStats = GetComponent<WeaponStats>();

        weaponInUseImage = GameObject.FindGameObjectWithTag("WeaponInUse").GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SetUpWeapon();
    }

    // Sets up the stats and the image of the object.
    void SetUpWeapon()
    {
        weaponID = weaponStates.GetChosenWeaponID();

        AbstractWeapon weaponsArray = weapons[weaponID];

        weaponInUseImage.sprite = weaponsArray.GetWeaponImage();

        weaponStats.SetRequestFromActualWeapon(true);
        weaponStats.CalculateStats();
        
        speed = weaponStats.GetSpeed();
        impactDamage = weaponStats.GetImpactDamage();
    }

    public int GetChosenID() { return weaponID; }
    public int GetWeaponSpeed() { return speed; }
    public int GetWeaponImpactDamage() { return impactDamage; }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

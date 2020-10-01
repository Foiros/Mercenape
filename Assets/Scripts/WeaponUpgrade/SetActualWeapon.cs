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

    [SerializeField] private float speed, impactDamage;
    
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
        int weaponID = weaponStates.GetChosenWeaponID();

        AbstractWeapon weaponsArray = weapons[weaponID];

        weaponInUseImage.sprite = weaponsArray.GetWeaponImage();

        weaponStats.SetRequestFromActualWeapon(true);
        weaponStats.CalculateStats();
        
        speed = weaponStats.GetSpeed();
        impactDamage = weaponStats.GetImpactDamage();
    }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
    public float GetWeaponSpeed() { return speed; }
    public float GetWeaponImpactDamage() { return impactDamage; }
}

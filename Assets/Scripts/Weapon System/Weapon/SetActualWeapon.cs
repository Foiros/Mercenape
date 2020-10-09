using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 16.9.2020: Script that set ups the weapon in game screen.
public class SetActualWeapon : MonoBehaviour
{
    private WeaponStates weaponStates;
    private StatsCalculator calculator;

    private List<AbstractWeapon> weapons;

    [SerializeField] private float speed, impactDamage;
    
    void Awake()
    {
        weaponStates = GetComponent<WeaponStates>();
        calculator = GetComponent<StatsCalculator>();
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

        GameObject weaponModel = weaponsArray.GetWeaponModel();

        weaponModel.SetActive(true);

        calculator.SetRequestFromActualWeapon(true);
        calculator.CalculateStats();
        
        speed = calculator.GetSpeed();
        impactDamage = calculator.GetImpactDamage();
    }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
    public float GetWeaponSpeed() { return speed; }
    public float GetWeaponImpactDamage() { return impactDamage; }
}

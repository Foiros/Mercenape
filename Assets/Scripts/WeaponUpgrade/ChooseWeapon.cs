using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 16.9.2020: This class allows the player to choose his weapon from among the ones he has unlocked. 
public class ChooseWeapon : MonoBehaviour
{
    private AbstractWeapon[] weapons;

    void Awake()
    {
        SetUpWeaponsArray();
    }

    // As in BuyWeapons script, this function sets up the abstract objects array, which can then be used by the code.
    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, null, null, null);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, null, null, null);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, null, null, null);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, null, null, null);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }
}

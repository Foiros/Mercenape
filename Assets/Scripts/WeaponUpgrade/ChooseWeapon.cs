﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Created by Arttu Paldán 16.9.2020: This class allows the player to choose his weapon from among the ones he has unlocked. 
public class ChooseWeapon : MonoBehaviour
{
    private WeaponStates weaponStates;
   
    private AbstractWeapon[] weapons;

    private int chosenWeaponID;
    private int buttonID;

    private string buttonName;

    public Image[] weaponHasBeenChosenHolder;
    public Sprite[] weaponHasBeenChosenImages;

    void Awake()
    {
        weaponStates = GetComponent<WeaponStates>();

        SetUpWeaponsArray();
    }

    // As in BuyWeapons script, this function sets up the abstract objects array, which can then be used by the code.
    void SetUpWeaponsArray()
    {
        TestWeapon1 testWeapon1 = new TestWeapon1("Weapon 1", "Does things", 0, 50, 5, 10, null, null, weaponHasBeenChosenImages[0]);
        TestWeapon2 testWeapon2 = new TestWeapon2("Weapon 2", "Does things", 1, 25, 1, 20, null, null, weaponHasBeenChosenImages[1]);
        TestWeapon3 testWeapon3 = new TestWeapon3("Weapon 3", "Does things", 2, 100, 3, 3, null, null, weaponHasBeenChosenImages[2]);
        TestWeapon4 testWeapon4 = new TestWeapon4("Weapon 4", "Does things", 3, 150, 10, 2, null, null, weaponHasBeenChosenImages[3]);

        weapons = new AbstractWeapon[] { testWeapon1, testWeapon2, testWeapon3, testWeapon4 };
    }

    // Button function, which detects which button has been pressed and gives us the chosenWeaponID based on that. 
    public void PlayersChoice()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;

        if (buttonName == "ChooseButton1" && weaponStates.ownsWeapon1)
        {
            buttonID = 0;
            chosenWeaponID = weapons[0].GetID();
            ChangeWeaponImage();
            weaponStates.SetChosenWeaponID(chosenWeaponID);
        }
        else if (buttonName == "ChooseButton2" && weaponStates.ownsWeapon2)
        {
            buttonID = 1;
            chosenWeaponID = weapons[1].GetID();
            ChangeWeaponImage();
            weaponStates.SetChosenWeaponID(chosenWeaponID);
        }
        else if (buttonName == "ChooseButton3" && weaponStates.ownsWeapon3)
        {
            buttonID = 2;
            chosenWeaponID = weapons[2].GetID();
            ChangeWeaponImage();
            weaponStates.SetChosenWeaponID(chosenWeaponID);
        }
        else if (buttonName == "ChooseButton4" && weaponStates.ownsWeapon4)
        {
            buttonID = 3;
            chosenWeaponID = weapons[3].GetID();
            ChangeWeaponImage();
            weaponStates.SetChosenWeaponID(chosenWeaponID);
        }
    }

    // Function that simply changes the image of an weapon to indicate, that this one is one the player is equipping. 
    void ChangeWeaponImage()
    {
        if (buttonName == "ChooseButton1")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetInUseImage();
        }
        else if (buttonName == "ChooseButton2")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetInUseImage();
        }
        else if (buttonName == "ChooseButton3")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetInUseImage();
        }
        else if (buttonName == "ChooseButton4")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetInUseImage();
        }
    }
}

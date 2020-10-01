using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Created by Arttu Paldán 16.9.2020: This class allows the player to choose his weapon from among the ones he has unlocked. 
public class ChooseWeapon : MonoBehaviour
{
    private WeaponStates weaponStates;
    private UseUpgrades useUpgrades;
   
    private List<AbstractWeapon> weapons;

    [SerializeField] private int chosenWeaponID;
    private int buttonID;

    private string buttonName;

    public Image[] weaponHasBeenChosenHolder;

    void Awake()
    {
        weaponStates = GetComponent<WeaponStates>();
        useUpgrades = GetComponent<UseUpgrades>();
    }

    void Start()
    {
        SetOwnedWeaponImages();
    }

    void SetOwnedWeaponImages()
    {
        List<bool> ownedWeapons = weaponStates.GetOwnedWeapons();

        chosenWeaponID = weaponStates.GetChosenWeaponID();

        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            switch (ownedWeapons[i])
            {
                case true:
                    weaponHasBeenChosenHolder[i].sprite = weapons[i].GetWeaponImage();
                    break;

                case false:
                    weaponHasBeenChosenHolder[i].sprite = null;
                    break;
            }
        }

        weaponHasBeenChosenHolder[chosenWeaponID].sprite = weapons[chosenWeaponID].GetChosenWeaponImage();
    }



    // Button function, which detects which button has been pressed and gives us the chosenWeaponID based on that. 
    public void PlayersChoice()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;

        List<bool> ownedWeaponsList = weaponStates.GetOwnedWeapons();

        if (buttonName == "ChooseButton1" && ownedWeaponsList[0])
        {
            buttonID = 0;
            chosenWeaponID = weapons[0].GetID();
            ChangeWeaponImage();
        }
        else if (buttonName == "ChooseButton2" && ownedWeaponsList[1])
        {
            buttonID = 1;
            chosenWeaponID = weapons[1].GetID();
            ChangeWeaponImage();
        }
        else if (buttonName == "ChooseButton3" && ownedWeaponsList[2])
        {
            buttonID = 2;
            chosenWeaponID = weapons[2].GetID();
            ChangeWeaponImage();
        }
        else if (buttonName == "ChooseButton4" && ownedWeaponsList[3])
        {
            buttonID = 3;
            chosenWeaponID = weapons[3].GetID();
            ChangeWeaponImage();
        }

        weaponStates.SetChosenWeaponID(chosenWeaponID);
        SaveManager.SaveWeapons(weaponStates);
        useUpgrades.SetUpUpgradeScreen();
    }

    // Function that simply changes the image of an weapon to indicate, that this one is one the player is equipping. 
    void ChangeWeaponImage()
    {
        if (buttonName == "ChooseButton1")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetChosenWeaponImage();

            SwitchOtherImages();
        }
        else if (buttonName == "ChooseButton2")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetChosenWeaponImage();

            SwitchOtherImages();
        }
       else if (buttonName == "ChooseButton3")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetChosenWeaponImage();

            SwitchOtherImages();
        }
        else if (buttonName == "ChooseButton4")
        {
            weaponHasBeenChosenHolder[buttonID].sprite = weapons[chosenWeaponID].GetChosenWeaponImage();

            SwitchOtherImages();
        }
    }

    void SwitchOtherImages()
    {
        List<bool> ownedWeaponsList = weaponStates.GetOwnedWeapons();


        switch (chosenWeaponID)
        {
            case 0:
                if (ownedWeaponsList[1])
                {
                    weaponHasBeenChosenHolder[1].sprite = weapons[1].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[1].sprite = null;
                }
                
                if(ownedWeaponsList[2])
                {
                    weaponHasBeenChosenHolder[2].sprite = weapons[2].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[2].sprite = null;
                }

                if (ownedWeaponsList[3])
                {
                    weaponHasBeenChosenHolder[3].sprite = weapons[3].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[3].sprite = null;
                }
                break;

            case 1:
                if (ownedWeaponsList[0])
                {
                    weaponHasBeenChosenHolder[0].sprite = weapons[0].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[0].sprite = null;
                }

                if (ownedWeaponsList[2])
                {
                    weaponHasBeenChosenHolder[2].sprite = weapons[2].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[2].sprite = null;
                }

                if (ownedWeaponsList[3])
                {
                    weaponHasBeenChosenHolder[3].sprite = weapons[3].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[3].sprite = null;
                }
                break;

            case 2:
                if (ownedWeaponsList[0])
                {
                    weaponHasBeenChosenHolder[0].sprite = weapons[0].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[0].sprite = null;
                }

                if (ownedWeaponsList[1])
                {
                    weaponHasBeenChosenHolder[1].sprite = weapons[1].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[1].sprite = null;
                }

                if (ownedWeaponsList[3])
                {
                    weaponHasBeenChosenHolder[3].sprite = weapons[3].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[3].sprite = null;
                }
                break;

            case 3:
                if (ownedWeaponsList[0])
                {
                    weaponHasBeenChosenHolder[0].sprite = weapons[0].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[0].sprite = null;
                }

                if (ownedWeaponsList[1])
                {
                    weaponHasBeenChosenHolder[1].sprite = weapons[1].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[1].sprite = null;
                }

                if (ownedWeaponsList[2])
                {
                    weaponHasBeenChosenHolder[2].sprite = weapons[2].GetWeaponImage();
                }
                else
                {
                    weaponHasBeenChosenHolder[2].sprite = null;
                }
                break;
        }
    }

    public void SetWeaponList(List<AbstractWeapon> list) { weapons = list; }
}

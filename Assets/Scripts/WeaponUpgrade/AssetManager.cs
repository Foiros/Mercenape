using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public Sprite[] weaponImages;
    public Sprite[] chosenWeaponImages;
    public Sprite[] upgradeImages;

    void Awake()
    {
        SetUpSprites();
    }

    void SetUpSprites()
    {
        weaponImages = Resources.LoadAll<Sprite>("Weapons");
        chosenWeaponImages = Resources.LoadAll<Sprite>("ChosenWeapons");
        upgradeImages = Resources.LoadAll<Sprite>("Upgrades");
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by ???
public class EnemyLootDrop : MonoBehaviour
{
    public GameObject floatMoney;
    public GameObject floatKarma;
    public GameObject floatUpgrade;
    
    //Ossi: These are the % chance of Health and Upgrade drops.
    public int healthChance = 40;
    public int upgradeChance = 30;

    //The value of HP and upgrade orbs. I'm thinking that each enemy has a different multiplier on the value of the orbs they spawn upon death. Something that's swirling in my noggin'.
    public int HPvalue = 15, UPvalue = 1;

    private void InstantiateKarmaDrop()
    {
        Instantiate(floatKarma, transform.position, Quaternion.identity);
    }

    private void InstantiateGoldDrop()
    {
        Instantiate(floatMoney, transform.position, Quaternion.identity);
    }
    
    private void InstantiateUpgrade()
    {
        int randomSpawn = Random.Range(0, 101);
        if (randomSpawn <= upgradeChance)
        {
            Instantiate(floatUpgrade, transform.position, Quaternion.identity);
        } 
    }

     public void GiveLoot()
     {
        InstantiateGoldDrop();
        InstantiateKarmaDrop();
        InstantiateUpgrade();
     }
}

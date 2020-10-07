using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class KarmaBar : MonoBehaviour
{

    public Slider karmaBar;
    public Image karmaFill;
    private PlayerCurrency playerCurrency;
    GameMaster gm;


    void Start()
    {

        karmaBar = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("karmaBar").GetComponent<Slider>();
        karmaFill = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("karmaBar").Find("karmaFill").GetComponent<Image>();

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        
       
        gm = GameObject.FindGameObjectWithTag("Player").GetComponent<GameMaster>();
        

        SetMaxValue();
    }

 

    void SetMaxValue()
    {
        if (gm != null)
        {
            karmaBar.maxValue = gm.lvMaxKarma;
            print("set Karma max value: " + gm.lvMaxKarma);
           
        }
        else { 
        karmaBar.maxValue = 1000;
        print("cant find gm");
        }
        karmaBar.value = playerCurrency.playerKarma;

    }

   public void SetValue()
    {
        karmaBar.value = playerCurrency.playerKarma;
    }
}

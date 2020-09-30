using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class KarmaBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    private PlayerCurrency playerCurrency;
    
    public GameObject karma;
    KarmaPickup karmaPickup;

    GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        playerCurrency = GetComponent<PlayerCurrency>();
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<GameMaster>();

        SetMaxValue();

        karmaPickup = karma.GetComponent<KarmaPickup>();
        karmaPickup.OnPlayerColKarma += KarmaPickup_OnPlayerColKarma;
    }

    private void KarmaPickup_OnPlayerColKarma(object sender, EventArgs e)
    {
        SetValue();
    }

   
    void SetMaxValue()
    {
        slider.maxValue = gm.lvMaxKarma;
        slider.value = playerCurrency.playerKarma;

    }

    void SetValue()
    {
        slider.value = playerCurrency.playerKarma;
    }
}

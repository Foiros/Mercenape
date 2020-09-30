using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KarmaCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    Text karmaCount;
    public GameObject karma;
    KarmaPickup karmaPickup;
    void Start()
    {
       
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        karmaCount = GetComponent<Text>();
        Updatetext();
        karmaPickup = karma.GetComponent<KarmaPickup>();
        karmaPickup.OnPlayerColKarma += KarmaPickup_OnPlayerColKarma;

    }

    private void KarmaPickup_OnPlayerColKarma(object sender, EventArgs e)
    {
        Updatetext();
    }

    // Update is called once per frame
    void Updatetext()
    {
        karmaCount.text = playerCurrency.playerKarma.ToString();
    }
}

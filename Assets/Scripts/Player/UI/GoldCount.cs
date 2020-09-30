using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    Text text;
    public GameObject gold;
    GoldDrop goldDrop;
        void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        text = GetComponent<Text>();
        TextUpdate();


        goldDrop = gold.GetComponent<GoldDrop>();
        goldDrop.OnPlayerColGold += GoldDrop_OnPlayerColGold;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TextUpdate();
        }
    }
    private void GoldDrop_OnPlayerColGold(object sender, EventArgs e)
    {
        TextUpdate();
    }



    // Update is called once per frame
    void TextUpdate()
    {
        text.text = playerCurrency.playerGold.ToString();
    }
}

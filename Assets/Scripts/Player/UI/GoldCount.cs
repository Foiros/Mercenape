using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    public Text moneyText;

      
    
    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        moneyText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("moneyText").GetComponent<Text>();

        // goldDrop = gold.GetComponent<GoldDrop>();
        // goldDrop.OnPlayerColGold += GoldDrop_OnPlayerColGold;
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


   // public void TextUpdate()
    // Update is called once per frame
    void TextUpdate()

    {
        moneyText.text = playerCurrency.playerGold.ToString();
    }
}

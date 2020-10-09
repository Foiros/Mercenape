using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatintMoneyText : MonoBehaviour
{
    GameObject floatingMoneyText;
    public int moneyAmount;
    PlayerCurrency playerCurrency;
    playerUI playerUI;

    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        playerUI = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").GetComponent<playerUI>();


        moneyAmount = Random.Range(10, 100);
        floatingMoneyText = transform.GetChild(0).gameObject;
        floatingMoneyText.GetComponent<TextMesh>().text = moneyAmount.ToString();

        playerCurrency.playerGold += moneyAmount;
        playerUI.MoneyText();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

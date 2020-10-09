using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingKarmaText : MonoBehaviour
{
    GameObject floatingKarmaText;
    public int karmmaAmount;
    PlayerCurrency playerCurrency;
    playerUI playerUI;

    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        playerUI = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").GetComponent<playerUI>();



        floatingKarmaText = transform.GetChild(0).gameObject;
        floatingKarmaText.GetComponent<TextMesh>().text = karmmaAmount.ToString();

        playerCurrency.playerKarma += karmmaAmount;
        playerUI.KarmaText();
        playerUI.SetKarmaValue();



    }
}

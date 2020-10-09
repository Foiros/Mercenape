using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUpgrade : MonoBehaviour
{
    PlayerCurrency playerCurrency;
    playerUI playerUI;
    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        playerUI = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").GetComponent<playerUI>();

        playerCurrency.playerUpgrade++;
        playerUI.UpgradeText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

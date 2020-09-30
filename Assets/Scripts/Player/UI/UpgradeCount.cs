using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UpgradeCount : MonoBehaviour
{
    private PlayerCurrency playerCurrency;
    Text upgradeCount;
    public GameObject upgrade;
    UpgradeDrop upgradeDrop;
    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        upgradeCount = GetComponent<Text>();
        UpdateText();

        upgradeDrop = upgrade.GetComponent<UpgradeDrop>();
        upgradeDrop.OnPlayerColUp += UpgradeDrop_OnPlayerColUp;

    }

    private void UpgradeDrop_OnPlayerColUp(object sender, EventArgs e)
    {
        UpdateText();
    }

    // Update is called once per frame
    void UpdateText()
    {
        upgradeCount.text = playerCurrency.playerUpgrade.ToString();
    }
}

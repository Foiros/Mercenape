using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeCount : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCurrency playerCurrency;
    Text upgradeCount;

    void Start()
    {
        playerCurrency = GetComponent<PlayerCurrency>();

        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

        upgradeCount = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        upgradeCount.text = playerCurrency.playerUpgrade.ToString();
    }
}

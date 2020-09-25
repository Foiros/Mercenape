using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public GameObject Player;
    public static PlayerCurrency playerCurrency;
    public int playerKarma;
    public int playerGold;
    public int playerUpgrade;

    void Awake()
    {
        LoadSaveFile();
    }

    void LoadSaveFile()
    {
        CurrencyData data = SaveManager.LoadCurrency();

        playerKarma = data.playerKarma;
        playerGold = data.playerMoney;
        playerUpgrade = data.speedUpgrades;
    }
}

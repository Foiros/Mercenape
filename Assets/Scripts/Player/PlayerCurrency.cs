using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

// Created by Thuyet Pham.
// Edited by Arttu Paldán 29.10.2020: Basically I just merged all the diffrent counting script into a one. 
public class PlayerCurrency : MonoBehaviour
{
    private GameMaster gameMaster;

    public int karma, gold, speedUpgrades;
    
    public Text moneyText, upgradeText, karmaText;
    
    public Slider karmaBar;
    public Image karmaFill;

    void Awake()
    {
        gameMaster = GetComponent<GameMaster>();

        LoadSaveFile();
    }

    void Start()
    {
        SetTexts();
        SetKarmaBar();
    }

    void SetTexts()
    {
        moneyText.text = gold.ToString();
        upgradeText.text = speedUpgrades.ToString();
    }

    public void SetKarmaBar()
    {
        if (gameMaster != null)
        {
            karmaBar.maxValue = gameMaster.lvMaxKarma;
        }
        else
        {
            karmaBar.maxValue = 1000;

        }

        karmaBar.value = karma;
        karmaText.text = karma.ToString();
    }

    public void AddGold(int amount)
    {
        gold += amount;

        moneyText.text = gold.ToString();
    }

    public void AddKarma(int amount)
    {
        karma += amount;

        karmaText.text = karma.ToString();
    }

    public void AddUpgrades(int amount)
    {
        speedUpgrades += amount;

        upgradeText.text = speedUpgrades.ToString();
    }

    public void LoseGold(int amount)
    {
        gold -= amount;

        moneyText.text = gold.ToString();
    }

    public void LoseKarma(int amount)
    {
        karma -= amount;

        karmaText.text = karma.ToString();
    }

    void LoadSaveFile()
    {
        CurrencyData data = SaveManager.LoadCurrency();

        if (data != null)
        {
            karma = data.playerKarma;
            gold = data.playerMoney;
            speedUpgrades = data.speedUpgrades;
        }
    }
}

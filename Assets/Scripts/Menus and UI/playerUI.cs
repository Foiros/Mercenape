using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerUI : MonoBehaviour
{
    private PlayerCurrency playerCurrency;
    private PlayerStat playerStat;

    public Text upgradeText;
    public Text moneyText;
    public Text karmaText;

    public Slider karmaBar;
    public Image karmaFill;

    public Slider hpBar;
    public Gradient hpGradient;
    public Image hpFill;


    GameMaster gm;


    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        gm = GameObject.FindGameObjectWithTag("Player").GetComponent<GameMaster>();
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        upgradeText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("upgradeText").GetComponent<Text>();
        moneyText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("moneyText").GetComponent<Text>();
        karmaText = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("karmaBar").Find("karmaText").GetComponent<Text>();
        karmaBar = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("karmaBar").GetComponent<Slider>();
        karmaFill = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("karmaBar").Find("karmaFill").GetComponent<Image>();
        hpBar = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("hpBar").GetComponent<Slider>();
        hpFill = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("hpBar").Find("hpFill").GetComponent<Image>();


        UpgradeText();// update the UI when start the game
        MoneyText();
        KarmaText();
        SetMaxValue();
        SetMaxHp();

    }


    // Update is called once per frame
    public void UpgradeText()
    {
        upgradeText.text = playerCurrency.playerUpgrade.ToString();
        upgradeText.color = Color.red;
    }

    public void MoneyText()

    {
        moneyText.text = playerCurrency.playerGold.ToString();
        moneyText.color = Color.red;
    }

    public void KarmaText()
    {
        karmaText.text = playerCurrency.playerKarma.ToString();
        karmaText.color = Color.red;
    }

    void SetMaxValue()
    {
        if (gm != null)
        {
            karmaBar.maxValue = gm.lvMaxKarma;
        }
        else
        {
            karmaBar.maxValue = 1000;
           
        }
        karmaBar.value = playerCurrency.playerKarma;

    }

    public void SetKarmaValue()
    {
        karmaBar.value = playerCurrency.playerKarma;
    }

    public void SetMaxHp()
    {
        hpBar.maxValue = playerStat.PlayerMaxHP;
        hpBar.value = playerStat.PlayerHP;
        hpFill.color = hpGradient.Evaluate(1f);
    }

    public void SetCurrentHP(float HP)
    {
        hpBar.value = HP;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }
}

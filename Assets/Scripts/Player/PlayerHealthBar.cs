using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider hpBar;
    public Gradient hpGradient;
    public Image hpFill;
    
    private PlayerStat playerStat;


    private void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("hpBar").GetComponent<Slider>();

        hpFill = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerUI").Find("hpBar").Find("hpFill").GetComponent<Image>();

        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        SetMaxHp();
    }

    public void SetMaxHp()
    {
        hpBar.maxValue = playerStat.PlayerMaxHP;
        hpBar.value =playerStat.PlayerHP ;
        hpFill.color = hpGradient.Evaluate(1f);
    }

    public void SetCurrentHP(float HP)
    {
        hpBar.value = HP;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] PlayerStat playerStat;
    
    private void Start()
    {
         SetMaxHp();
        
    }
    private void Update()
    {
        SetCurrentHP(playerStat.PlayerHP);
    }

    public void SetMaxHp()
    {
        slider.maxValue = playerStat.PlayerMaxHP;
        slider.value =playerStat.PlayerHP ;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetCurrentHP(int HP)
    {

        slider.value = HP;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealthBar : MonoBehaviour
{
   
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    private PlayerStat playerStat;

    public GameObject hpDrop;
    HealthOrbDrop healthOrbDrop;
    private void Start()
    {
        
        
        playerStat = GetComponent<PlayerStat>();
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        SetMaxHp();
        healthOrbDrop = hpDrop.GetComponent<HealthOrbDrop>();
        healthOrbDrop.OnPlayerColHP += HealthOrbDrop_OnPlayerColHP;
    }

    private void HealthOrbDrop_OnPlayerColHP(object sender, EventArgs e)
    {
        SetCurrentHP(playerStat.PlayerHP);    }

    public void SetMaxHp()
    {
        slider.maxValue = playerStat.PlayerMaxHP;
        slider.value =playerStat.PlayerHP ;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetCurrentHP(float HP)
    {

        slider.value = HP;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}

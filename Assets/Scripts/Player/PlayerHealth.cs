using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created by Thuyet Pham
// Edited by Arttu Paldán on 29.10.2020: Mainly I just merged this and PlayerHealthBar into one script. 
public class PlayerHealth : MonoBehaviour
{
    private PlayerCurrency playerCurrency;

    public int PlayerHP;
    public int PlayerMaxHP;

    private Slider hpBar;
    private Image hpFill;
    public Gradient hpGradient;
    
    public int lostGold;
    public int lostKarma;


    void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerCurrency>();

        hpBar = GameObject.FindGameObjectWithTag("PlayerUI").transform.Find("hpBar").GetComponent<Slider>();
        hpFill = GameObject.FindGameObjectWithTag("PlayerUI").transform.Find("hpBar").Find("hpFill").GetComponent<Image>();

        SetHP();
    }

    void SetHP()
    {
        PlayerHP = PlayerMaxHP;
        hpBar.maxValue = PlayerMaxHP;
        hpBar.value = PlayerHP;
        hpFill.color = hpGradient.Evaluate(1f);
    }

    // This can be handled by the UpdateHealth().
    public void PlayerTakeDamage(int EnemyDamage)
    {
        PlayerHP -= EnemyDamage;
        SetCurrentHP(PlayerHP);
        CheckPlayerDeath();
    }

    public void GainHealth(int gain)
    {
        PlayerHP += gain;
        SetCurrentHP(PlayerHP);
    }

    void SetCurrentHP(float HP)
    {
        hpBar.value = HP;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }

    void CheckPlayerDeath()
    {
        if (PlayerHP <= 0)
        {
            LoseCurrency();
          
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void LoseCurrency()
    {
        lostGold = playerCurrency.gold * 10 / 100;
        lostKarma = playerCurrency.karma * 10 / 100;

        playerCurrency.LoseGold(lostGold);

        playerCurrency.LoseKarma(lostKarma);
        
        SaveManager.SaveCurrency(playerCurrency);
    }
}

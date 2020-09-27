using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Arttu Paldán 25.9.2020: Temprorary system for changing the scene when required. 
public class LoadScene : MonoBehaviour
{
    private PlayerCurrency playerCurrency;

    void Awake()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
    }

    public void GoTolevel1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Bao_Enemy");
    }

    public void GoToForge()
    {
        Time.timeScale = 1;
        SaveManager.SaveCurrency(playerCurrency);
        SceneManager.LoadScene("Arttu_WeaponSystem");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SaveManager.SaveCurrency(playerCurrency);
        SceneManager.LoadScene("mainMenu");
    }
}

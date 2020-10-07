using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Arttu Paldán 25.9.2020: Temprorary system for changing the scene when required. 
public class LoadScene : MonoBehaviour
{
    private PlayerCurrency playerCurrency;
    GameMaster gamemaster;
    private EnemySpawnerScript spawner;

    void Awake()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        gamemaster = GameObject.FindGameObjectWithTag("Player").GetComponent<GameMaster>();
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerScript>();
    }

    public void GoTolevel1()
    {
        if (spawner.state == EnemySpawnerScript.SpawnState.Counting)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("LV1");
        }
    }

    public void GoTolevel1FromMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LV1");
        
    }

    public void GoToForge()
    { //check if player have enough karma
        if (playerCurrency.playerKarma >= gamemaster.lvMaxKarma && spawner.state == EnemySpawnerScript.SpawnState.Counting)
        {
            Time.timeScale = 1;
            SaveManager.SaveCurrency(playerCurrency);
            SceneManager.LoadScene("Arttu_WeaponSystem");
        }
        //place holder
        else 
        { 
            print("cant go"); 
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SaveManager.SaveCurrency(playerCurrency);
        SceneManager.LoadScene("mainMenu");
    }
}

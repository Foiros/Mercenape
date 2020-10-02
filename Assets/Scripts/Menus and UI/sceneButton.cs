using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneButton : MonoBehaviour
{
    [SerializeField]
    private Object Scenetoload;
    private PlayerCurrency playerCurrency;
    GameMaster gamemaster;
    private void Awake()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        gamemaster = GameObject.FindGameObjectWithTag("Player").GetComponent<GameMaster>();
    }
    public void changeScene()
    {
        //check if player have enough karma
        if (playerCurrency.playerKarma >= gamemaster.lvMaxKarma)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(Scenetoload.name);
        }
    
        //place holder
        else 
        { 

            print("cant go");
        }
    }
}

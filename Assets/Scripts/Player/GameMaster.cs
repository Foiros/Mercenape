using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
   public int lvMaxKarma;
    PlayerCurrency playerCurrency;

    // Start is called before the first frame update
    void Start()
    {
        CheckScene4MaxKarma();
        
    }

    // Update is called once per frame

    private void Update()
    {
        SwitchLV();
    }

    void CheckScene4MaxKarma()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        string sceneName = currentScene.name;

        int buildIndex = currentScene.buildIndex;




        if (buildIndex > 0)
        {
            lvMaxKarma = buildIndex * 1000;
        }
        else
        {
            lvMaxKarma = 500;
        }
    }

    void SwitchLV()
    {

        if (playerCurrency.playerKarma >= lvMaxKarma)
        {
           
            if (Input.GetKeyDown(KeyCode.H))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{

    //These vectors point to where each panel except menuCenter goes to when disabled. Sure, it may not be necessary, but I like the filing cabinet aesthetic.
    // I unwittingly made this so compact, that I added the pause menu on the same script. I will chang the name of the script ASAP since it is doing my head in.
    public bool isPaused = false;
    public Canvas mainCanvas;
    public GameObject[] panels;
    public Vector2[] startPos;
    public GameObject currentPanel;

    public GameObject selectedButton;
    bool isSelected = false;

    void Start()
    {
        

        
        mainCanvas = transform.GetComponent<Canvas>();
        startPos = new Vector2[panels.Length];
        currentPanel = panels[0];
        
        //Set the position each panel returns to when not selected.
        for(int i = 0; i < panels.Length; i++)
        {
            startPos[i] = panels[i].transform.position;
            Debug.Log(startPos[i]);           
        }
        panels[0].transform.position = mainCanvas.transform.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
        if(isSelected)
        {
            if(Input.anyKeyDown)
            {
                selectedButton.GetComponentInChildren<Text>().text = Input.inputString;
                isSelected = false;
            }
        }
    }

    public void pauseGame()
    {
        if(!isPaused)
        {
            
            for(int i = 0; i < panels.Length; i++)
            {
                if (panels[i].name.Contains("pause"))
                {
                    panels[i].transform.position = mainCanvas.transform.position;
                    currentPanel = panels[i];
                    isPaused = true;
                    break;
                }
            }
            Time.timeScale = 0;
        } else
        {
            currentPanel = null;
            isPaused = false;
            Time.timeScale = 1;
        }
    }



    public void switchPanel()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        //Find the the Currentpanel and return it to its starting position.
        for(int i = 0; i < panels.Length; i++)
        {
            if(currentPanel = panels[i])
            {
                
                currentPanel.transform.position = startPos[i];
                for (int j = 0; j < panels.Length; j++)
                {
                    //Then we find the chose panel by the name of the button.
                    if (panels[j].name.Contains(buttonName))
                    {
                        Debug.Log("Found the next panel, " + panels[j].name);
                        panels[j].transform.position = mainCanvas.transform.position;
                        currentPanel = panels[j];
                        break;
                    } else { Debug.LogError("Panel not found!"); }
                }
                break;
            }        
        }
    }

    public void backButton()
    {
        GameObject panelParent;

        if (!isPaused)
        {
            //This is a bit clucky, but it checks of the pause menu is active.
            panelParent = currentPanel.transform.parent.gameObject;
            for (int i = 0; i < panels.Length; i++)
            {
                if (currentPanel.name.Contains(panels[i].name))
                {
                    panelParent.transform.position = mainCanvas.transform.position;
                    currentPanel.transform.position = startPos[i];
                    currentPanel = panelParent;
                    break;
                }
            }

        } else
        {
            //If paused, we'll find the pausepanel and designate it as the "parent". Not really since I want to avoid excess spaghetti.
            for(int i = 0; i < panels.Length; i++)
            {
                if(panels[i].name.Contains("pause"))
                {
                    panelParent = panels[i];
                    for (int j = 0; j < panels.Length; j++)
                    {
                        if (currentPanel.name.Contains(panels[i].name))
                        {
                            panelParent.transform.position = mainCanvas.transform.position;
                            currentPanel.transform.position = startPos[j];
                            currentPanel = panelParent;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void changeInput()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        Debug.Log(selectedButton.name);
        selectedButton.GetComponentInChildren<Text>().text = "Add new input";
        isSelected = true;
        
    }public void changeScene()
    {

    }


    public void startGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("SampleScene");
        //Load game Scene
    }

    public void quitGame()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }
}

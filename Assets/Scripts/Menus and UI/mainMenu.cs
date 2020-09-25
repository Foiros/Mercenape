using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// Arttu paldán edited 25.9.2020: 
public class mainMenu : MonoBehaviour
{

    //These vectors point to where each panel except menuCenter goes to when disabled. Sure, it may not be necessary, but I like the filing cabinet aesthetic.
    // I unwittingly made this so compact, that I added the pause menu on the same script. I will chang the name of the script ASAP since it is doing my head in.
    public bool isPaused = false;
    public Canvas mainCanvas;
    public GameObject[] panels;
    public Vector2[] startPos;
    public GameObject currentPanel;
    public GameObject pausePanel;

    public GameObject selectedButton;
    bool isSelected = false;

    void Start()
    {       
        mainCanvas = transform.GetComponent<Canvas>();
        startPos = new Vector2[panels.Length];  
        //Set the position each panel returns to when not selected.
        for(int i = 0; i < panels.Length; i++)
        {
            if (panels[i].name.Contains("pause"))
            {
                pausePanel = panels[i];              
            }
            startPos[i] = panels[i].transform.position;         
        }
        panels[0].transform.position = mainCanvas.transform.position;
        currentPanel = panels[0];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause pressed");
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

    //This returns the current panel to its starting position when a new panel is put up. 
    private void returnPanel(GameObject panelReturn)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            if(panels[i] == panelReturn)
            {
                panelReturn.transform.position = startPos[i];
                break;
            }
        }
    }

    public void pauseGame()
    {
        //First we check if a pausepanel's been assigned.
        if (pausePanel != null)
        {
            if (!isPaused)
            {
                //Pause the game
                if (currentPanel != null)
                {
                    returnPanel(currentPanel);
                }
                pausePanel.transform.position = mainCanvas.transform.position;
                isPaused = true;
                Time.timeScale = 0;
                currentPanel = pausePanel;
            }
            else
            {
                //Resume the game
                returnPanel(currentPanel);
                isPaused = false;
                Time.timeScale = 1;
                currentPanel = null;
            }
        }

    }



    public void switchPanel()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        //Return the current panel to its startPos.
        returnPanel(currentPanel);
        for (int i = 0; i < panels.Length; i++)
        {

            if (panels[i].name.Contains(buttonName))
            {
                Debug.Log(panels[i].name + " found.");
                //If the game is paused, Options and Level select panels are set as Pausepanel's children so that the BackButton() works properly
                if (panels[i].name.Contains("options") || panels[i].name.Contains("level"))
                {
                    if (!isPaused)
                    {
                        panels[i].transform.SetParent(panels[0].transform, false);
                        currentPanel = panels[i];
                    }
                    else
                    {
                        panels[i].transform.SetParent(pausePanel.transform, false);
                        currentPanel = panels[i];
                    }
                
                }
                if(panels[i] == panels[0])
                {
                    isPaused = false;
                    Time.timeScale = 1;
                }
                panels[i].transform.position = mainCanvas.transform.position;
                currentPanel = panels[i];
                Debug.Log("Current panel: " + currentPanel.name);
                break;
            }
        }
    }

    public void backButton()
    {
        returnPanel(currentPanel);
        for(int i = 0; i < panels.Length; i++)
        {
            if(panels[i] == currentPanel.transform.parent.gameObject)
            {
                Debug.Log("Panel parent found: " + panels[i]);
                panels[i].transform.position = mainCanvas.transform.position;            
                currentPanel = panels[i];
                break;
            }
        }
    }

    public void changeInput()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        Debug.Log(selectedButton.name);
        selectedButton.GetComponentInChildren<Text>().text = "Add new input";
        isSelected = true;
    }
        
    public void startGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("Bao_Enemy");
        //Load game Scene
    }

    public void quitGame()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }

    public void WipeMemory()
    {
        SaveManager.DeleteWeapons();
    }
}

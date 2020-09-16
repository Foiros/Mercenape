using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{

    //These vectors point to where each panel except menuCenter goes to when disabled. Sure, it may not be necessary, but I like the filing cabinet aesthetic.
    public Canvas mainCanvas;
    public GameObject[] panels;
    public Vector2[] startPos;
    public GameObject currentPanel;
    public int testNumber;
    public Button[] inputButtons;
    public GameObject inputHolder;

    public GameObject selectedButton;
    bool isSelected = false;

    void Start()
    {
        inputButtons = new Button[GameObject.FindGameObjectsWithTag("inputButton").Length];
        inputHolder = GameObject.Find("inputHolder");
        if(inputHolder != null)
        {
            inputButtons[0] = GameObject.FindGameObjectWithTag("inputButton").GetComponent<Button>();
            for(int i = 0; i < inputButtons.Length; i++)
            {
                inputButtons[i] = GameObject.FindWithTag("inputButton").gameObject.GetComponent<Button>();
            }
        }

        
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
        if(isSelected)
        {
            if(Input.anyKeyDown)
            {
                selectedButton.GetComponentInChildren<Text>().text = Input.inputString;
                isSelected = false;
            }
        }
    }



    public void switchPanel()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        //Find the panel and set it aside...
        for(int i = 0; i < panels.Length; i++)
        {
            if(currentPanel = panels[i])
            {
                //After the old panel is set aside, then place the new panel on the screen.
                currentPanel.transform.position = startPos[i];
                for (int j = 0; j < panels.Length; j++)
                {
                    if (panels[j].name.Contains(buttonName))
                    {
                        Debug.Log("Found the next panel, " + panels[j].name);
                        panels[j].transform.position = mainCanvas.transform.position;
                        currentPanel = panels[j];
                        break;
                    }
                }
                break;
            }        
        }
    }

    public void backButton()
    {
        GameObject panelParent = currentPanel.transform.parent.gameObject;
        Debug.Log(panelParent.name);
        if (panelParent != null)
        {
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
        SceneManager.LoadScene("SampleScene");
        //Load game Scene
    }

    public void quitGame()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }
}

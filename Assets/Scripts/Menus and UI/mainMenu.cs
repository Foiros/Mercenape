using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{

    //These vectors point to where each panel except menuCenter goes to when disabled. Sure, it may not be necessary, but I like the filing cabinet aesthetic.
    public GameObject[] panels;
    public Vector2[] startPos;
    public GameObject currentPanel;
    public GameObject previousPanel;
    public int testNumber;

    void Start()
    {
      
        startPos = new Vector2[panels.Length];
        currentPanel = panels[0];
        for(int i = 0; i < panels.Length; i++)
        {
            startPos[i] = panels[i].transform.position;
            Debug.Log(startPos[i]);           
        }
        
    }

    public void switchPanel()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        previousPanel = currentPanel;
        Debug.Log(buttonName);
        Debug.Log("Looking for panel..");
        for(int i = 0; i < panels.Length; i++)
        {  
            if(panels[i].name.Contains(buttonName))
            {
                if(panels[i] != panels[0])
                {
                    panels[0].SetActive(false);
                } else
                {
                    panels[0].SetActive(true);
                }
                Debug.Log("Panel found");
                panels[i].transform.position = startPos[0];
                currentPanel = panels[i];
            }
            
        }
    }

    public void backButton()
    {
        //Here we find the parent of the parent of the button. A grand-parent, if you will.
        GameObject parentPanel = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        GameObject grandparentPanel = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.transform.parent.gameObject;;
        if (parentPanel && grandparentPanel != null)
        {
            Debug.Log("Parent: " + parentPanel.name);
            Debug.Log("Grandparent: " + grandparentPanel.name);
            //If both are not null, the panel rewind only once
            for (int i = 0; i > panels.Length; i++)
            {
                if (panels[i] == parentPanel)
                {
                    parentPanel.transform.position = startPos[i];
                    grandparentPanel.transform.position = startPos[0];
                    grandparentPanel.SetActive(true);
                    currentPanel = grandparentPanel;
                    parentPanel = null;
                    grandparentPanel = null;
                }
            }
        }
        else if (parentPanel != null && grandparentPanel == null)
        {
            Debug.Log("No grandparent found");
            //If there is no grandparent, then it means that the main panel is next on the line.
            for(int i = 0; i > panels.Length; i++)
            {
                if(parentPanel.name == panels[i].name)
                {
                    parentPanel.transform.position = startPos[i];
                    parentPanel.SetActive(true);
                    currentPanel = panels[0];
                    parentPanel = null;
                    grandparentPanel = null;
                }
            }
            
        }
        //Reset the panels for the next Back-button press
        parentPanel = null;
        grandparentPanel = null;
        
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

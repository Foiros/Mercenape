using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButton : MonoBehaviour
{
    public GameObject paneltoOpen;

    // Start is called before the first frame update
    
    public void changePanel()
    {
        mainMenu menu = transform.parent.transform.parent.GetComponent<mainMenu>();
        if(menu != null)
        {
            Debug.Log("Menu component found " + paneltoOpen.name);
            //Checking pause           
            menu.switchPanel(paneltoOpen);
        }
    }
}

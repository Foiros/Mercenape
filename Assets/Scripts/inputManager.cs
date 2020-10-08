using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class inputManager : MonoBehaviour
{
    //Written by Ossi Uusitalo
    //This script is put on the UI_Canvas for ease of access.
    public KeyCode[] inputs, defaultInputs;
    public GameObject[] inputButtons;
    public GameObject selectedInput;
    //You can set the then inputs from the editor
    [SerializeField]
    public KeyCode left, right, up, down, jump;


    void Start()
    {
        //You could declare the inputs in editor, but there are so many of them to go through, that it's quicker to do in code.
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        jump = KeyCode.Space;

        //Make an array for the inputs to be compared when a button is pressed. I know this isn't as intuitive, but this is the best I could come up with.
        //Also, make sure that these inputs are in the same top-to-bottom, left-to-right order as they are on the menu panel. We do not want to mix them up. Nothing fatal though.
        inputs = new KeyCode[5];
        inputs[0] = left;
        inputs[1] = right;
        inputs[2] = up;
        inputs[3] = down;
        inputs[4] = jump;

        //Make sure to add the inputbuttons array in the same order as the array above.

        //Then we declare that these are the default setting that can be set on the input menu.
        defaultInputs = new KeyCode[inputs.Length];

        // Here we fill the default array
        if(defaultInputs != null)
        {
            for(int i = 0; i < defaultInputs.Length; i++)
            {
                defaultInputs[i] = inputs[i];

            }
        }
        //I know that with more inputs, I'll have to update this. Until I find a smoother solution, I'll do it manually. 

    }

    public void changeInput(string inputName)
    {
        for(int i = 0; i < inputButtons.Length; i++)
        {

        }
    }


    public void clearInput(string inputClear)
    {
        
    }

    public void setDefault()
    {
        // Just to check if the defaults set in.
        for(int i = 0; i < inputs.Length; i++)
        {

            Debug.Log(inputs[i].ToString());
        }

        //for(int i = 0; i < inputs.Length; i++)
        //{
        //    defaul
        //}
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            KeyCode currentKey = e.keyCode;
            
            if (selectedInput != null)
            {
                for(int i = 0; i < inputButtons.Length; i++)
                {

                }
            }
            else
            {
                if (currentKey == left)
                {
                    Debug.Log("Left pressed");
                }

                if (currentKey == right)
                {
                    Debug.Log("Right pressed");
                }
                if (currentKey == up)
                {
                    Debug.Log("Up pressed");
                }
                if (currentKey == down)
                {
                    Debug.Log("Down pressed");
                }
                if (currentKey == jump)
                {
                    Debug.Log("Jump pressed");
                }
            }
        }
    
    }
}

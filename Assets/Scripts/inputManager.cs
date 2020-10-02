using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class inputManager : MonoBehaviour
{
    //This script is put on the UI_Canvas for ease of access.
    public KeyCode[] inputs;
    //You can set the then inputs from the editor
    [SerializeField]
    public KeyCode left, right, up, down, jump;
    void Start()
    {
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        jump = KeyCode.Space;

        //Make an array for the inputs to be compared when a button is pressed. I know this isn't as intuitive, but this is the best I could come up with.
        inputs = new KeyCode[5];
        inputs[0] = left;
        inputs[1] = right;
        inputs[2] = up;
        inputs[3] = down;
        inputs[4] = jump;
        //I know that with more inputs, I'll have to update this. Until I find a smoother solution, I'll do it manually. 

    }

    //private void OnGUI()
    //{
    //    Event e = Event.current;
    //    if(e.isKey)
    //    {
    //        KeyCode currentKey = e.keyCode;
    //        if(currentKey == left)
    //        {
    //            Debug.Log("Left pressed");
    //        }
    //        
    //        if (currentKey == right)
    //        {
    //            Debug.Log("Right pressed");
    //        }
    //        if (currentKey == up)
    //        {
    //            Debug.Log("Up pressed");
    //        }
    //        if (currentKey == down)
    //        {
    //            Debug.Log("Down pressed");
    //        }
    //        if (currentKey == jump)
    //        {
    //            Debug.Log("Jump pressed");
    //        }
    //
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneButton : MonoBehaviour
{
    [SerializeField]
    private Object Scenetoload;
    // Start is called before the first frame update
    public void changeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Scenetoload.name);
    }
}

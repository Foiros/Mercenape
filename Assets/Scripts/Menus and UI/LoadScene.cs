using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void GoTolevel1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Bao_Enemy");
    }

    public void GoToForge()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Arttu_WeaponSystem");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenu");
    }
}

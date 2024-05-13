using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDMainMenu : MonoBehaviour
{
    public void OnClicQuit()
    {
        Application.Quit();
    }

    public void OnClicStart() 
    {
        SceneManager.LoadScene("Game_Farm");
    }
}

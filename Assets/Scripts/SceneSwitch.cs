using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
         SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        //Attach to a button, to quit the game when compiled
        Application.Quit();
    }
}

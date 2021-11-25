using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickTransition : MonoBehaviour
{
    public GameObject video;
    void Update()
    {
        //Switch scenes from title screen to login
        //This is the only scene that should take this input
        //so it shouldn't be harmful to hardcode the transition
        if (Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene("ChooseLogScene");
            Destroy(video);
        }
    }
}

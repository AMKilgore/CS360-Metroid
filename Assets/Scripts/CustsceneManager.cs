using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustsceneManager : MonoBehaviour
{

    public string cutSceneName;
    public bool hasPlayed = false;
    

    public void IntroCutscene()
    {
        // Freeze player
        GameObject.Find("Player").GetComponent<PlayerMovement>().isFrozen = true;
        // Start song
        GameObject.Find("Music").GetComponent<MusicManager>().ChangeMusic("IntroCutscene");
        // Activate animation
        GameObject.Find("Player").GetComponent<Animator>().SetBool("PlayIntro", true);
        // Activate delay
        Invoke("IntroDelay", 7.0f);
    }

    private void IntroDelay()
    {
        // Change audio clip back, and return control to player
        GameObject.Find("Music").GetComponent<MusicManager>().ChangeMusic("Brinstar");
        FindObjectOfType<PlayerMovement>().isFrozen = false;
        //GameObject.Destroy(this.gameObject);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("PlayIntro", false);
    }
}

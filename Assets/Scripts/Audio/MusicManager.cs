using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public string currentClip;
    // Start is called before the first frame update
    void Start()
    {
        // Check if music is already playing, do not start a new copy of music if it is.
        // Hacky method, but it works
        var c = new GameObject("check");
        DontDestroyOnLoad(c);
        foreach (var root in c.scene.GetRootGameObjects())
        {
            if (root.name == "Music")
            {
                Destroy(c);
                Destroy(this.gameObject);
            }
        }
        // Destroy the dummy object
        Destroy(c);

        // Check if Music exists already
        if (gameObject.scene.buildIndex == -1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMusic()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying) return;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public void ChangeMusic(string name)
    {
        // Change audio and play new clip
        this.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Music/" + name);
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}

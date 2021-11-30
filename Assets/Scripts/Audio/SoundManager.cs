using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public void PlaySound(string name)
    {
        // Change audio and play new clip
        this.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Sounds/" + name);
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}

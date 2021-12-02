using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public GameObject pauseDisplay;

    // Start is called before the first frame update
    void Start()
    {
        pauseDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            pauseDisplay.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            pauseDisplay.SetActive(false);
        }
    }
}

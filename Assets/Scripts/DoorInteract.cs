using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public string doorLocation;
    SceneTracker st;

    // Start is called before the first frame update
    void Start()
    {
        st = GameObject.FindObjectOfType(typeof(SceneTracker)) as SceneTracker;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        st.FadeToScene(doorLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

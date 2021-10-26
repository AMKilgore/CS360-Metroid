using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public string doorLocation;
    // 0 = bullet, 1 = missle
    public string openType;
    SceneTracker st;

    // Start is called before the first frame update
    void Start()
    {
        st = GameObject.FindObjectOfType(typeof(SceneTracker)) as SceneTracker;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get name of collider, remove the clone identifier if a missle/bullet
        string collider = other.name.Split('(')[0];
        
        if (collider == "Player")
            st.FadeToScene(doorLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

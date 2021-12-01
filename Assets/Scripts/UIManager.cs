using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var c = new GameObject("check1");
        DontDestroyOnLoad(c);
        foreach (var root in c.scene.GetRootGameObjects())
        {
            if (root.name == "UI")
            {
                Destroy(c);
                // Update to use correct camera
                Camera camera = null;
                if (GameObject.Find("Main Camera Y Locked") != null)
                {
                    camera = GameObject.Find("Main Camera Y Locked").GetComponent<Camera>();
                }
                else
                {
                    camera = GameObject.Find("Main Camera X Locked").GetComponent<Camera>();
                }

                root.GetComponent<Canvas>().worldCamera = camera;
                Debug.Log("d");

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

    // Update the GUI number display
    void UpdateUINumber(SpriteRenderer icon, int value)
    {
        icon.sprite = Resources.Load<Sprite>("UI/" + value.ToString());
    }
}

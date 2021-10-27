using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedX : MonoBehaviour
{
    private float xPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Transform cameraPos = gameObject.GetComponent(typeof(Transform)) as Transform;
        player = GameObject.Find("Player");
        xPos = cameraPos.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerPos = gameObject.GetComponentInParent(typeof(Transform)) as Transform;
        this.transform.position = new Vector3(xPos, player.transform.position.y, -10.0f);

        // Look for other items if player is null
        if (player == null)
        {
            Object[] go = GameObject.FindObjectsOfType(typeof(MonoBehaviour));
            for (int i = 0; i < go.Length; i++)
            {
                string n = go[i].name.Split('(')[0];
                if (n == "MorphBall")
                    player = go[i] as GameObject;
            }
            player = GameObject.Find("MorphBall");
        }
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedY : MonoBehaviour
{
    private float yPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Transform cameraPos = gameObject.GetComponent(typeof(Transform)) as Transform;
        player = GameObject.Find("Player");
        yPos = cameraPos.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(playerPos.position.x, 1.61f, -10.0f);
        this.transform.position = new Vector3(player.transform.position.x, yPos, -10.0f);
    }

    private void LateUpdate()
    {
        //this.transform.position = new Vector3(this.transform.position.x, 6.5f, this.transform.position.z);
        this.transform.position = new Vector3(this.transform.position.x, yPos, this.transform.position.z);
    }

}

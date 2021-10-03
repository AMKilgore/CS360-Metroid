using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedY : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerPos = gameObject.GetComponentInParent(typeof(Transform)) as Transform;
        this.transform.position = new Vector3(playerPos.position.x, 1.61f, -10.0f);
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, 6.5f, this.transform.position.z);
    }

}

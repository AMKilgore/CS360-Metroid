using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerPos = gameObject.GetComponentInParent(typeof(Transform)) as Transform;
        this.transform.position = new Vector3(5.97f, playerPos.position.y, -10.0f);
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(5.97f, this.transform.position.y, this.transform.position.z);
    }
}

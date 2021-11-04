using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer_Movement : MonoBehaviour
{

    public float speed;

    public bool MoveRight;

    

    int n = 2;

    // Update is called once per frame
    void Update()
    {

        
        if (MoveRight)
        {
            transform.Translate(n * Time.deltaTime * speed, 0, 0); // move right
        }
        else
        {
            transform.Translate(-n * Time.deltaTime * speed, 0, 0); // move left
        }

        if (transform.position.x >= 70.0) // RH point
        {
            MoveRight = false;
        }

        if (transform.position.x <= 40.0) // LH point
        {
            MoveRight= true;
        }
        
    }
}

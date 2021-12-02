using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_6 : MonoBehaviour
{
    public int health = 5;
    private float speed = 1.0f;
    public bool MoveRight;

    // Update is called once per frame
    void Update()
    {
        if (MoveRight)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0); // move right
        }
        else
        {
            transform.Translate(-Time.deltaTime * speed, 0, 0); // move left
        }

        if (transform.position.x >= 35) // RH point
        {
            MoveRight = false;
        }

        if (transform.position.x <= -3) // LH point
        {
            MoveRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string collider = other.collider.name.Split('(')[0];
        if (collider == "normalShot")
            health -= 1;
        else if (collider == "missle")
            health -= 2;
        else
            return;

        //Destroy(other);

        if (health == 0)
            Destroy(gameObject);
    }
}
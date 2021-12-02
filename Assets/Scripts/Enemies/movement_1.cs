using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_1 : MonoBehaviour
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

        if (transform.position.x >= 70.0) // RH point
        {
            MoveRight = false;
        }

        if (transform.position.x <= 40.0) // LH point
        {
            MoveRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string collider = other.collider.name.Split('(')[0];
        if (collider == "normalShot")
            health -= 1;
            // take damage
        else if (collider == "missle")
            health -= 2;
            // take damage
        else
            return;

        if (health == 0)
            // death animation
            Destroy(gameObject);
    }
}
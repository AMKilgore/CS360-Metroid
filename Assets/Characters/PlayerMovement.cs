using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 3.4f;
    float horizontalDirection;
    float gravityScale = 2.0f;
    Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement, -1 indicates a left movement, 1 indicates a right movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
        }
    }

    void FixedUpdate()
    {
        // Calculate movement velocity
        //$$ 0 is a placeholder on the y axis movement
        r2d.velocity = new Vector2((horizontalDirection) * horizontalSpeed, 0);
    }
}

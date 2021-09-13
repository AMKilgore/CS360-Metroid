using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 3.4f;
    public float verticalSpeed = 50.0f;
    public bool isGrounded = true;
    float horizontalDirection;
    float verticalDirection;
    float gravityScale = 1.0f;
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
        else
        {
            horizontalDirection = 0;
        }

        // Vertical movement
        if (Input.GetKey(KeyCode.Z) && isGrounded)
        {
            verticalDirection = 1;
            r2d.AddForce(Vector2.up * 50.0f);
        }
    }

    void FixedUpdate()
    {
        // Calculate movement velocity
        //$$ 0 is a placeholder on the y axis movement
        //r2d.velocity = new Vector2((horizontalDirection) * horizontalSpeed, 0);
        //r2d.MovePosition(new Vector2((horizontalDirection * horizontalSpeed * Time.deltaTime) + r2d.position.x,
        //    (verticalDirection * verticalSpeed * Time.deltaTime/4) + r2d.position.y));

        //r2d.MovePosition(new Vector2((horizontalDirection * horizontalSpeed * Time.deltaTime) + r2d.position.x,
        //    r2d.position.y));
        r2d.transform.Translate(Vector2.right * horizontalDirection * horizontalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        Debug.Log("grounded");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        Debug.Log("not grounded");
    }
}

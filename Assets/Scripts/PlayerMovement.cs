using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float horizontalSpeed = 3.4f;
    public float verticalSpeed = 50.0f;
    public bool isGrounded = true;
    float horizontalDirection;
    float playerFacing;
    float verticalDirection;
    float gravityScale = 1.0f;

    Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Horizontal movement, -1 indicates a left movement, 1 indicates a right movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
            animator.SetFloat("Direction", horizontalDirection);
        }
        else
        {
            horizontalDirection = 0;
        }

        // Move horizontally
        r2d.transform.Translate(Vector2.right * horizontalDirection * horizontalSpeed * Time.deltaTime);

        // Set the walking animation
        if (horizontalDirection != 0 && isGrounded)
        {
            animator.SetFloat("Move X", horizontalDirection);
            animator.SetBool("IsMoving", true);
        }
        // Standing still on the ground
        else if (horizontalDirection == 0 && isGrounded)
        {
            animator.SetBool("IsMoving", false);
        }

        // Vertical movement
        if (Input.GetKey(KeyCode.Z) && isGrounded)
        {
            verticalDirection = 1;
            r2d.AddForce(Vector2.up * 300.0f);
        }

        // Logic for playing any jumping animations
        if (r2d.velocity.y != 0 && isGrounded)
        {
            // Falling
            if (r2d.velocity.y < 0 || horizontalDirection == 0)
            {
                if (animator.GetFloat("Direction") == 1)
                    animator.SetFloat("Move X", 0.3f);
                else
                    animator.SetFloat("Move X", -0.3f);
            }
            // Activate the jump animation
            animator.SetBool("IsJumping", true);
            isGrounded = false;
        }
        else if (!isGrounded && r2d.velocity.y == 0)
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
           
    }
}

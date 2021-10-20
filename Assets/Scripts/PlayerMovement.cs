using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float horizontalSpeed = 3.4f;
    public float verticalSpeed = 50.0f;
    public bool isGrounded = true;
    float horizontalDirection;
    float verticalDirection;
    float gravityScale = 1.0f;
    Rigidbody2D r2d;

    Collider2D lastTouchedCollider;

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
            r2d.AddForce(Vector2.up * 10.0f);
        }
    }

    void FixedUpdate()
    {
        r2d.transform.Translate(Vector2.right * horizontalDirection * horizontalSpeed * Time.deltaTime);
        // Right walk animation
        if (horizontalDirection == 1)
        {
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Walk");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("grounded");
        }

        lastTouchedCollider = other;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lastTouchedCollider != null)
        {
            if (isGrounded && lastTouchedCollider.tag == "Ground")
            {
                isGrounded = false;
                Debug.Log("not grounded");
            }

            lastTouchedCollider = null;
        }
    }
}

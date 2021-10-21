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
    BoxCollider2D boxCollider2D;
    Collider2D lastTouchedCollider;

    private Tilemap tilemap;
    TilemapCollider2D a;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        a = GameObject.Find("Brinstar_Tiles").GetComponent<TilemapCollider2D>();

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
            playerFacing = 0;
        }
        else
        {
            if (horizontalDirection != 0)
                playerFacing = horizontalDirection;
            horizontalDirection = 0;
        }


        // Vertical movement
        if (Input.GetKey(KeyCode.Z) && isGrounded)
        {
            verticalDirection = 1;
            r2d.AddForce(Vector2.up * 10.0f);
            
        }
        else if (isGrounded)
        {
            verticalDirection = 0;
        }

        Debug.Log(isGroundedTest());
    }

    void FixedUpdate()
    {
        if (horizontalDirection != 0)
        {
            animator.SetFloat("Move X", horizontalDirection);
            animator.SetBool("IsMoving", true);
            
            r2d.transform.Translate(Vector2.right * horizontalDirection * horizontalSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Direction", playerFacing);
            animator.SetBool("IsMoving", false);
        }

        if (verticalDirection != 0)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
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

    private bool isGroundedTest()
    {
        RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.down, transform.position.y + 0.1f, 10);
        RaycastHit2D[] l = new RaycastHit2D[1];
        ContactFilter2D cf = new ContactFilter2D();
        int c = a.Raycast(Vector2.up, l, 0.5f);
        Debug.Log(c);
        Debug.Log(rh.collider);

        return Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + 0.1f, 10);
    }
}

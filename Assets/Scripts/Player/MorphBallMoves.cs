using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphBallMoves : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D r2d;

    public GameObject storedPlayer;

    // Parameters, taken from player
    public float horizontalSpeed = 3.4f;
    public float verticalSpeed = 50.0f;
    float horizontalDirection;
    float gravityScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    private void FixedUpdate()
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

        // Leave morphball, respawn player
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GameObject mb = Instantiate(storedPlayer, r2d.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Move horizontally
        r2d.transform.Translate(Vector2.right * horizontalDirection * horizontalSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

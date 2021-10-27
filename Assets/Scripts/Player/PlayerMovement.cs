using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float horizontalSpeed = 3.4f;
    public bool isGrounded = true;
    float horizontalDirection;
    float verticalDirection;
    float playerFacing;
    float gravityScale = 1.0f;

    // Shot types
    public GameObject normalShot;
    public int numberShots = 0;
    int fireDelay = 0;

    // Upgrades $$ Need to be changed once upgrades are added
    public bool hasMorphBall = true;
    public GameObject MorphBall;

    Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        normalShot = (GameObject)Resources.Load("normalShot", typeof(GameObject));
        MorphBall = (GameObject)Resources.Load("MorphBall", typeof(GameObject));

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    private void Awake()
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

        // Look up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalDirection = 1;
            animator.SetBool("LookUp", true);
        }
        else
        {
            verticalDirection = 0;
            animator.SetBool("LookUp", false);
        }

        if (!animator.GetBool("IsMorphBall") && Input.GetKey(KeyCode.DownArrow))
        {
            CreateMorphBall();
            //animator.SetBool("IsMorphBall", true);
        }
        else if (animator.GetBool("IsMorphBall") && Input.GetKey(KeyCode.UpArrow))
        {
            //animator.SetBool("IsMorphBall", false);
        }

        // Fire the weapon if the player is grounded
        if (Input.GetKey(KeyCode.X) && !animator.GetBool("IsFlipJumping") && fireDelay == 5)
        {
            Fire();
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
                animator.SetBool("IsFlipJumping", false);
            }
            else
                animator.SetBool("IsFlipJumping", true);
            // Activate the jump animation
            animator.SetBool("IsJumping", true);
            isGrounded = false;
        }
        // Ground has been hit
        if (!isGrounded && r2d.velocity.y == 0)
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }

        // Delay for the missle firing, if the user holds down the fire button, only allow to fire every five frames
        if (fireDelay == 5)
            fireDelay = 0;
        else
            ++fireDelay;
    }

    void Fire()
    {
        //$$ Add implementation for the different shot types here, use switches?
        float d = animator.GetFloat("Direction");
        bool v = animator.GetBool("LookUp");
        GameObject ns;
        if (!v)
            ns = Instantiate(normalShot, r2d.position + (Vector2.up * 0.40f) + (Vector2.right * d * 0.5f), Quaternion.identity);
        else
            ns = Instantiate(normalShot, r2d.position + (Vector2.up * 0.40f) + (Vector2.right * d * 0.3f * 0.45f), Quaternion.identity);
        NormalProjectilePhysics nsPhys = ns.GetComponent<NormalProjectilePhysics>();

        nsPhys.Fire(d, v);
        // Add number shots if needed to check 
        ++numberShots;
    }

    void CreateMorphBall()
    {
        GameObject mb = Instantiate(MorphBall, r2d.position, Quaternion.identity);
        MorphBallMoves mbMoves = mb.GetComponent<MorphBallMoves>();
        Destroy(gameObject);
    }
}

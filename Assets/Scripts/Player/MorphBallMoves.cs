using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphBallMoves : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D r2d;

    public GameObject storedPlayer;

    // Delay between placing bombs
    private int bombDelay = 0;
    private bool canPlaceBomb;

    // Upgrades, change later
    public bool hasMorphBallBomb = true;
    public GameObject morphBallBomb;
    public int numMorphBombs = 0;
    MorphBallBomb morphBallBScript;

    // Parameters, taken from player
    public float horizontalSpeed = 3.4f;
    public float verticalSpeed = 50.0f;
    float horizontalDirection;
    float gravityScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        morphBallBomb = (GameObject)Resources.Load("MorphBallBomb", typeof(GameObject));
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
        }
        else
        {
            horizontalDirection = 0;
        }

        // Place bomb
        if (Input.GetKey(KeyCode.X) && numMorphBombs < 3 && canPlaceBomb)
        {
            PlaceMorphBallBomb();
            canPlaceBomb = false;
        }

        // Leave morphball, respawn player
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GameObject mb = Instantiate(storedPlayer, r2d.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Move horizontally
        r2d.transform.Translate(Vector2.right * horizontalDirection * horizontalSpeed * Time.deltaTime);

        if (!canPlaceBomb && bombDelay < 10)
        {
            bombDelay += 1;
        }
        else if (!canPlaceBomb && bombDelay == 10)
        {
            canPlaceBomb = true;
            bombDelay = 0;
        }
    }

    // Place Morph Ball bomb
    void PlaceMorphBallBomb()
    {
        GameObject mbb = Instantiate(morphBallBomb, r2d.position, Quaternion.identity);
        morphBallBScript = mbb.GetComponent<MorphBallBomb>();
        ++numMorphBombs;
    }

}

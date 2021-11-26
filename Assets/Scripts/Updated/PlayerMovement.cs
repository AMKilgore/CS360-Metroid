using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    //Additional variables for saving
    public string username;
    public string currentScene;
    //health
    //energy tanks
    //missiles
    //longbeam
    //morphball
    public float[] location = new float[2];
    public int highscore;
    public bool admin;
    //
    
    public Animator animator;

    public float horizontalSpeed = 3.4f;
    public bool isGrounded = true;
    float horizontalDirection;
    float verticalDirection;
    float playerFacing;
    float gravityScale = 1.0f;

    // Player Attributes
    public int health = 99;
    public int totalEnergyTanks = 0;
    public int remainingEnergyTanks = 0;
    public int maxMissles = 5;
    public int numMissles = 5;

    // Shot types
    enum FireMode { normal, missle };
    FireMode selectedMode;
    bool canChange = true;
    int changeDelay = 0;

    public GameObject normalShot;
    public int numberShots = 0;
    public int maxShots = 3;
    bool canFire = true;
    int fireDelay = 0;
    // Shot upgrades $$ Change later
    public bool hasLongBeam = true;

    // Upgrades $$ Need to be changed once upgrades are added
    public bool hasMorphBall = false;
    public GameObject MorphBall;

    // Missles, need one for each direction
    public GameObject leftMissle;
    public GameObject rightMissle;
    public GameObject upMissle;

    public Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        normalShot = (GameObject)Resources.Load("normalShot", typeof(GameObject));
        MorphBall = (GameObject)Resources.Load("MorphBall", typeof(GameObject));
        leftMissle = (GameObject)Resources.Load("missleLeft", typeof(GameObject));
        rightMissle = (GameObject)Resources.Load("missleRight", typeof(GameObject));
        upMissle = (GameObject)Resources.Load("missleUp", typeof(GameObject));

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        normalShot = (GameObject)Resources.Load("normalShot", typeof(GameObject));
        MorphBall = (GameObject)Resources.Load("MorphBall", typeof(GameObject));
        leftMissle = (GameObject)Resources.Load("missleLeft", typeof(GameObject));
        rightMissle = (GameObject)Resources.Load("missleRight", typeof(GameObject));
        upMissle = (GameObject)Resources.Load("missleUp", typeof(GameObject));

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
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

        // Change firemode
        if (Input.GetKeyUp(KeyCode.A) && canChange)
        {
            if (selectedMode == FireMode.normal)
            {
                animator.SetBool("InMissleMode", true);
                selectedMode = FireMode.missle;
            }
            else
            {
                animator.SetBool("InMissleMode", false);
                selectedMode = FireMode.normal;
            }
            canChange = false;
        }

        if (!canChange)
        {
            if (changeDelay == 10)
            {
                changeDelay = 0;
                canChange = true;
            }
            else
                ++changeDelay;
        }
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

        if (!animator.GetBool("IsMorphBall") && Input.GetKey(KeyCode.DownArrow) && hasMorphBall)
        {
            CreateMorphBall();
            //animator.SetBool("IsMorphBall", true);
        }

        // Fire the weapon if the player is grounded
        if (Input.GetKey(KeyCode.X) && !animator.GetBool("IsFlipJumping") && canFire)
        {
            Debug.Log(selectedMode);
            Fire(selectedMode);
            canFire = false;
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
            animator.SetBool("IsFlipJumping", false);
            animator.SetBool("IsJumping", false);
        }

        // Delay for the missle firing, if the user holds down the fire button, only allow to fire every five frames
        if (!canFire)
        {
            if (fireDelay == 5)
            {
                fireDelay = 0;
                canFire = true;
            }
            else
                ++fireDelay;
        }

    }

    void Fire(FireMode mode)
    {
        //$$ Add implementation for the different shot types here, use switches?
        float d = animator.GetFloat("Direction");
        bool v = animator.GetBool("LookUp");

        if (FireMode.normal == mode && ((hasLongBeam && numberShots < maxShots) || !hasLongBeam))
        {   
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
        else if (FireMode.missle == mode && (numberShots < maxShots) && numMissles > 0)
        {
            GameObject obj = null;

            // Vertical missle
            if (v)
                obj = upMissle;
            // Left missle
            else if (d == -1)
                obj = leftMissle;
            // Right missle
            else
                obj = rightMissle;

            GameObject ns;
            if (!v)
                ns = Instantiate(obj, r2d.position + (Vector2.up * 0.40f) + (Vector2.right * d * 0.5f), Quaternion.identity);
            else
                ns = Instantiate(obj, r2d.position + (Vector2.up * 0.40f) + (Vector2.right * d * 0.3f * 0.45f), Quaternion.identity);
            MissleProjectile mPhys = ns.GetComponent<MissleProjectile>();

            mPhys.Fire(d, v);
            // Add number shots if needed to check 
            numberShots += 3;

            // Decrease missle count
            UpdateMissles(-1);
        }
    }

    // Return true or false if health is changed
    public bool UpdateMissles(int val)
    {
        // Do not add missle if at maximum
        if (numMissles == maxMissles && val == 1)
        {
            return false;
        }

        numMissles += val;
        // Update the GUI value
        int h = numMissles / 100;
        int t = (numMissles % 100) / 10;
        int o = (numMissles % 100) % 10;
        UpdateUINumber(GameObject.Find("Missles_Hundreds").GetComponent<SpriteRenderer>(), h);
        UpdateUINumber(GameObject.Find("Missles_Tens").GetComponent<SpriteRenderer>(), t);
        UpdateUINumber(GameObject.Find("Missles_Ones").GetComponent<SpriteRenderer>(), o);
        return true;
    }

    // Return true or false if health is changed
    public bool UpdateHealth(int val)
    {
        health += val;
        //$$ Need to add UI update for this
        if (health > 99 && remainingEnergyTanks < totalEnergyTanks)
        {
            remainingEnergyTanks += 1;
            health -= 99;
        }
        // Health is max, do not add
        else if (health > 99 && remainingEnergyTanks == totalEnergyTanks)
        {
            health -= val;
            return false;
        }
        // Update the GUI value
        int t = health / 10;
        int o = health % 10;
        UpdateUINumber(GameObject.Find("Health_Tens").GetComponent<SpriteRenderer>(), t);
        UpdateUINumber(GameObject.Find("Health_Ones").GetComponent<SpriteRenderer>(), o);
        return true;
    }

    void CreateMorphBall()
    {
        GameObject mb = Instantiate(MorphBall, r2d.position, Quaternion.identity);
        MorphBallMoves mbMoves = mb.GetComponent<MorphBallMoves>();
        Destroy(gameObject);
    }

    // Update the GUI number display
    void UpdateUINumber(SpriteRenderer icon, int value)
    {
        icon.sprite = Resources.Load<Sprite>("UI/" + value.ToString());
    }



    //Saving & loading
    //add to button or function
    public void SavePlayer()
    {
        Saving.SavePlayer(this);

    }

    //add to button or function
    //implement currentScene and position variables
    public void LoadPlayer(string user)
    {
        SavedValues data = Saving.LoadPlayer(user);
        currentScene = data.currentScene;
        health = data.health;
        remainingEnergyTanks = data.remainingEnergyTanks;
        numMissles = data.numMissles;
        hasLongBeam = data.hasLongBeam;
        hasMorphBall = data.hasMorphBall;
        Vector2 position;
        position.x = data.location[0];
        position.y = data.location[1];
        transform.position = position;
        admin = data.admin;
        username = user;
    }

}

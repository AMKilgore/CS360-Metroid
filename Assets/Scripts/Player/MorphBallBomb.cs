using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphBallBomb : MonoBehaviour
{
    public Animator animation;
    private bool animPlaying = false;
    private float counter = 0;
    private float animLength;
    public bool isExploding = false;
    // Make sure multiple player collisions do not happen
    public bool hasLaunched = false;

    // Start is called before the first frame update
    void Awake()
    {
        isExploding = false;
        hasLaunched = false;
        animation = gameObject.GetComponent<Animator>();
        animLength = animation.GetCurrentAnimatorStateInfo(0).length;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for animation to finish before destroying object
        if (counter < animLength)
        {
            counter += Time.deltaTime;
            return;
        }
        Destroy(gameObject);
    }

    // Deiterate the morphball bomb on destruction
    private void OnDestroy()
    {
        FindObjectOfType<MorphBallMoves>().numMorphBombs--;
    }

    // Activate the collider to begin dealing damage/launching
    private void OnExplode()
    {
        isExploding = true;
        LaunchPlayer();
    }

    public void LaunchPlayer()
    {

        float f = 400.00f;
        if (GameObject.Find("Player") != null || GameObject.Find("Player(Clone)") != null)
        {
            // Get distance from player
            float dist = Vector2.Distance(FindObjectOfType<PlayerMovement>().transform.position, transform.position);
            if (dist < 1)
            {
                float sign = Mathf.Sign(FindObjectOfType<PlayerMovement>().transform.position.x - transform.position.x);
                FindObjectOfType<PlayerMovement>().r2d.AddForce((f * Vector2.up) + (f * Vector2.right * sign));
            }
        }
        else if (GameObject.Find("MorphBall") != null || GameObject.Find("MorphBall(Clone)") != null)
        {
            float dist = Vector2.Distance(FindObjectOfType<MorphBallMoves>().transform.position, transform.position);
            Debug.Log(dist);
            if (dist < 1)
            {
                float sign = Mathf.Sign(FindObjectOfType<MorphBallMoves>().transform.position.x - transform.position.x);
                if (dist < 0.05)
                    sign = 0.0f;
                FindObjectOfType<MorphBallMoves>().r2d.AddForce((f * Vector2.up) + (f * Vector2.right * sign));
            }
        }
    }
}

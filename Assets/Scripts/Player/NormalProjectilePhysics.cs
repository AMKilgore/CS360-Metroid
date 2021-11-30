using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectilePhysics : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    bool hasLongBeam = false;
    Vector3 spawnPosition;
    GameObject c;

    // Start is called before the first frame update
    void Awake()
    {
        // Play sound
        GameObject.Find("Sounds").GetComponent<SoundManager>().PlaySound("Shoot");

        hasLongBeam = FindObjectOfType<PlayerMovement>().hasLongBeam;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
        // Get camera for deleting when out of view
        c = GameObject.Find("Main Camera X Locked");
        if (c == null)
            c = GameObject.Find("Main Camera Y Locked");
    }

    private void OnDestroy()
    {
        FindObjectOfType<PlayerMovement>().numberShots--;
    }

    public void Fire(float direction, bool isUp)
    {
        // Horizontal direction
        if (!isUp)
            rb2d.AddForce(Vector2.right * direction * 400.0f);
        // Vertical direction
        else
            rb2d.AddForce(Vector2.up * 400.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("OnCollide", true);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLongBeam)
        {
            // 4 tiles is the default range, add logic for y direction
            if (Mathf.Abs(transform.position.x - spawnPosition.x) > 4)
            {
                Destroy(gameObject);
            }
        }

        // Radius of camera to edge
        float size = FindObjectOfType<Camera>().orthographicSize * 2f;
        // Check if outside of bounds of camera (X)
        if (gameObject.transform.position.x < c.transform.position.x - size || gameObject.transform.position.x > c.transform.position.x + size)
        {
            Destroy(gameObject);
        }
        // Check if outside of bounds of camera (Y)
        else if (gameObject.transform.position.y < c.transform.position.y - size || gameObject.transform.position.y > c.transform.position.y + size)
        {
            Destroy(gameObject);
        }
    }
}

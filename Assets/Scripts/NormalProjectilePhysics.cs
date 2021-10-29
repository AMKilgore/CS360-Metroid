using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectilePhysics : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    bool hasLongBeam = false;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Awake()
    {
        hasLongBeam = FindObjectOfType<PlayerMovement>().hasLongBeam;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
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
    }
}

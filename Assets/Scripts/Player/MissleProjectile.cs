using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleProjectile : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
    }

    private void OnDestroy()
    {
        FindObjectOfType<PlayerMovement>().numberShots -= 3;
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
        Debug.Log("destroy");
        animator.SetBool("OnCollide", true);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

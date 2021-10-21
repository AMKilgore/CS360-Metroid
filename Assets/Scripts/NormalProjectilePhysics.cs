using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectilePhysics : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Fire(float direction)
    {
        rb2d.AddForce(Vector2.right * direction * 400.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("OnCollide", true);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

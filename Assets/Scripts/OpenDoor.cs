using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;

    // 0 = bullet, 1 = missle
    public string openType;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get name of collider, remove the clone identifier if a missle/bullet
        string collider = other.name.Split('(')[0];

        Debug.Log(collider);

        if (collider == openType)
        {
            animator.SetInteger("OpenType", 1);
            //Destroy(gameObject);
        }
    }

    public void deleteItem()
    {
        Debug.Log("Delete");
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

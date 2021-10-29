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
        animator = GetComponentInParent<Animator>();
        animator.SetInteger("Direction", direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get name of collider, remove the clone identifier if a missle/bullet
        string collider = other.name.Split('(')[0];

        if (collider == openType)
        {
            animator.SetInteger("OpenType", 1);
            WaitForAnimationToFinish();
            Destroy(gameObject);
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

    // Function to wait for animation to finish
    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
}

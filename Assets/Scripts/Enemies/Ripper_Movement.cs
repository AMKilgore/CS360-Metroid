using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripper_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool left;
    Animator animator;
    BoxCollider2D boxcollider;
    Rigidbody2D m_Rigidbody;

    private float positionTracker1;
    private float positionTracker2;
    void Start()
    {
        //m_Rigidbody.gravityScale = 0;
        speed=3;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        m_Rigidbody.gravityScale=0;
        left=true;
        positionTracker1=450000;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(left){
            //m_Rigidbody.transform.Translate(Vector2.left * speed * Time.deltaTime);
            m_Rigidbody.MovePosition(transform.position + Vector3.left * Time.deltaTime * speed);
        }
        else{
            //m_Rigidbody.transform.Translate(Vector2.right * speed * Time.deltaTime);
            m_Rigidbody.MovePosition(transform.position + Vector3.right * Time.deltaTime * speed);
        }
        positionTracker2=positionTracker1;
        positionTracker1=m_Rigidbody.position.x;
        if(positionTracker1-positionTracker2 ==0)
        
        if(positionTracker1-positionTracker2 ==0){
            if(left){
                left=false;
                m_Rigidbody.MovePosition(transform.position + Vector3.right * Time.deltaTime * speed);
                animator.SetBool("left", false);
            }
            else{
                left=true;
                m_Rigidbody.MovePosition(transform.position + Vector3.left * Time.deltaTime * speed);
                animator.SetBool("left", true);
            }
        }
    }
}

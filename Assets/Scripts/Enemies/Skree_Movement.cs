using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skree_Movement : MonoBehaviour
{
    public bool isFalling;
    public bool hasFallen;
    GameObject playerObject;
    Rigidbody2D m_Rigidbody;
    Animator animator;
    public float speed = 15;
    public int health = 2;
    public float horizontalSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
        playerObject = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Rigidbody.drag = GetDragFromAcceleration(Physics.gravity.magnitude, 2);
        hasFallen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

        if(playerObject!=null){
        float distance = playerObject.transform.position.x - transform.position.x;
        //Debug.Log("Player position is: "+ playerObject.transform.position);
        //Debug.Log("Skree position is: "+this.transform.position);

        if (distance < 3 && distance > -3) 
        {
            m_Rigidbody.gravityScale = 1;
            if (distance < 0) 
            {
                if(!isFalling)
                    m_Rigidbody.transform.Translate(Vector2.down * speed * Time.deltaTime);
                if(!hasFallen)
                    m_Rigidbody.transform.Translate(Vector2.left * horizontalSpeed * Time.deltaTime);
                //m_Rigidbody.MovePosition(transform.position + Vector3.down * Time.deltaTime * speed);
                //m_Rigidbody.MovePosition(transform.position + Vector3.left * Time.deltaTime * horizontalSpeed);
                //transform.Translate(Vector3.down * Time.deltaTime * speed);
                //transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);

            }
            else 
            {
                if(!isFalling)
                    m_Rigidbody.transform.Translate(Vector2.down * speed * Time.deltaTime);
                if(!hasFallen)
                    m_Rigidbody.transform.Translate(Vector2.right * horizontalSpeed * Time.deltaTime);
                //transform.Translate(Vector3.down * Time.deltaTime * speed);
                //transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
            }
        m_Rigidbody.gravityScale=4;    
        animator.SetBool("isFalling", true);
        if(m_Rigidbody.velocity.y==0&&isFalling)
            hasFallen=true;
        isFalling=true;
        }
        }
        else{
            playerObject = GameObject.Find("Player(Clone)");
            if(playerObject==null)
                playerObject = GameObject.Find("MorphBall(Clone)");
        }
        if(hasFallen) {
            m_Rigidbody.bodyType = RigidbodyType2D.Static;
            StartCoroutine(waitFunction());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string other = collision.collider.name.Split('(')[0];
        if (other == "normalShot")
            health -= 1;
        else if (other == "missleRight" || other == "missleLeft" || other == "missleUp")
            health -= 2;

        if (health == 0)
            Destroy(gameObject);
    }

    public static float GetDrag(float aVelocityChange, float aFinalVelocity)
    {
        return aVelocityChange / ((aFinalVelocity + aVelocityChange) * Time.fixedDeltaTime);
    }
    public static float GetDragFromAcceleration(float aAcceleration, float aFinalVelocity)
    {
        return GetDrag(aAcceleration * Time.fixedDeltaTime, aFinalVelocity);
    }

IEnumerator waitFunction(){
    yield return new WaitForSeconds(2);
    Destroy(gameObject);
}

}


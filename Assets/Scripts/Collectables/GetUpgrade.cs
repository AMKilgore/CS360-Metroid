using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgrade : MonoBehaviour
{
    // Attributes to all upgrades
    public string upgradeName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Get first part of name of collision object
        string n = other.gameObject.name.Split('(')[0];

        // Make sure the player touched the item
        if (n == "Player")
        {
            // Find upgrade to give to player
            if (upgradeName == "MorphBall")
                FindObjectOfType<PlayerMovement>().hasMorphBall = true;

            // Play music here and freeze world

            GameObject.Destroy(this.gameObject);
        }
    }
}

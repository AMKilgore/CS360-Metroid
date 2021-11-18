using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgrade : MonoBehaviour
{
    // Attributes to all upgrades
    public string upgradeName;

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
            // Find upgrade to give to player
            else if (upgradeName == "LongBeam")
                FindObjectOfType<PlayerMovement>().hasLongBeam = true;

            // Play music here and freeze world

            GameObject.Destroy(this.gameObject);
        }
    }
}

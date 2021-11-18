using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPickup : MonoBehaviour
{
    // Attributes
    public string type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get first part of name of collision object
        string n = collision.gameObject.name.Split('(')[0];

        // Make sure the player touched the item
        if (n == "Player")
        {
            bool success = false;
            // Find pickup to give to player (5 health for basic enemies, 20 for strong)
            if (type == "Health")
            {
                success = FindObjectOfType<PlayerMovement>().UpdateHealth(5);
                Debug.Log("Health collected");
            }
            else if (type == "Missle" && FindObjectOfType<PlayerMovement>().numMissles < FindObjectOfType<PlayerMovement>().maxMissles)
            {
                success = FindObjectOfType<PlayerMovement>().UpdateMissles(1);
                Debug.Log("Missle collected");
            }
            // Only delete if item is added
            if (success)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}

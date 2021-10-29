using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableBlocks : MonoBehaviour
{
    private Tilemap tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyNearest(GameObject destroyer)
    {
        // Destroy all tiles within a 1 unit range from the explosion
        tiles.SetTile(tiles.WorldToCell(new Vector3(destroyer.transform.position.x, destroyer.transform.position.y - 1f, 0f)), null);
        tiles.SetTile(tiles.WorldToCell(new Vector3(destroyer.transform.position.x, destroyer.transform.position.y + 1f, 0f)), null);
        tiles.SetTile(tiles.WorldToCell(new Vector3(destroyer.transform.position.x -1, destroyer.transform.position.y, 0f)), null);
        tiles.SetTile(tiles.WorldToCell(new Vector3(destroyer.transform.position.x + 1, destroyer.transform.position.y, 0f)), null);
    }


}

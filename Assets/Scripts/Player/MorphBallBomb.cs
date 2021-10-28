using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphBallBomb : MonoBehaviour
{
    public Animator animation;
    private bool animPlaying = false;
    private float counter = 0;
    private float animLength;

    // Start is called before the first frame update
    void Awake()
    {
        animation = gameObject.GetComponent<Animator>();
        animLength = animation.GetCurrentAnimatorStateInfo(0).length;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for animation to finish before destroying object
        if (counter < animLength)
        {
            counter += Time.deltaTime;
            return;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FindObjectOfType<MorphBallMoves>().numMorphBombs--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is the big object of information that needs to persist for the player
//to pick up where they left off.
//Serializable allows this object to be serialized, i.e. binary serialization.
//information used originated from: https://www.youtube.com/watch?v=XOjd_qU2Ido
[System.Serializable]
public class SavedValues
{
    public string currentScene;
    public int health;
    public int remainingEnergyTanks;
    public int numMissles;
    public bool hasLongBeam; //long beam unlocked
    public bool hasMorphBall;
    public float[] location = new float[2];
    public int highscore;
    public bool admin;
    /*why save highscore separate when you can save a double deep array 
     * with usernames and highscores
     * which can be saved upon login if highscore > 0
     * compare through the array and put in order each time
    */

    public SavedValues(PlayerMovement player)
    {
        currentScene = player.currentScene;
        health = player.health;
        remainingEnergyTanks = player.remainingEnergyTanks;
        numMissles = player.numMissles;
        hasLongBeam = player.hasLongBeam;
        hasMorphBall = player.hasMorphBall;
        location[0] = player.transform.position.x;
        location[1] = player.transform.position.y;
        highscore = player.highscore;
        admin = player.admin;
    }



}

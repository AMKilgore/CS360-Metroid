using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
 /*Example player class.
   Get and set statements included

    To use: create a PlayerClass variable in PlayerMovement (PlayerClass player;)
    Remove the previously used variable placeholders for the values from PlayerMovement
    and wherever these values are used, whether to get the values or change the values,
     > Use playerVariableName.getThatVariable() to get the values
     > and playerVariableName.setThatVariable(new value) to set it to something new

    If wanted, the currentScene can be replaced with the combination of scene name and side of
    the room as discussed to be used for changing rooms in-game. Maybe in the form of a string 
    array which can then be used in the scene tracking to go directly to that scene upon load?

    This way all that should need to be done is the replacing of get/sets with the equivalent actions
    and this can load on its own in a separate game object, such as replacing the TempLoginObject, 
    which can then be placed as a variable in or sent to the PlayerMovement control object, and everything 
    can be accessed!

    Would also make saving a much more straightforward process as it would be saving this object, directly,
    instead of attempting to isolate the variables from PlayerMovement.
 */

    string username;
    string currentScene;
    int health;
    int remainingEnergyTanks;
    int numMissles;
    bool hasLongBeam;
    bool hasMorphball;
    //float[] location = new float[2];
    int highscore;
    bool admin;

    //set new players
    public PlayerClass(string username) //regular user
    {
        this.username = username;
        this.currentScene = "BrinstarEntrance";
        this.health = 99;
        this.remainingEnergyTanks = 0;
        this.numMissles = 0;
        this.hasLongBeam = false;
        this.hasMorphball = false;
        this.highscore = 0;
        this.admin = false;
    }

    //load player information
    PlayerClass(string user, string scene, int health, int tanks, int miss, bool lb, bool mb, int hs, bool admin)
    {
        this.username = user;
        this.currentScene = scene;
        this.health = health;
        this.remainingEnergyTanks = tanks;
        this.numMissles = miss;
        this.hasLongBeam = lb;
        this.hasMorphball = mb;
        this.highscore = hs;
        this.admin = admin;
    }

    public string getUsername()
    {
        return this.username;
    }
    public void setUsername(string username)
    {
        this.username = username;
    }

    public string getScene()
    {
        return this.currentScene;
    }
    public void setScene(string scene)
    {
        this.currentScene = scene;
    }

    public int getHealth()
    {
        return this.health;
    }
    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getTanks()
    {
        return this.remainingEnergyTanks;
    }
    public void setTanks(int num)
    {
        this.remainingEnergyTanks = num;
    }

    public int getMissles()
    {
        return this.numMissles;
    }
    public void setMissles(int num)
    {
        this.numMissles = num;
    }

    public bool getLongBeam()
    {
        return this.hasLongBeam;
    }
    public void setLongBeam(bool lb)
    {
        this.hasLongBeam = lb;
    }

    public bool getMorphBall()
    {
        return this.hasMorphball;
    }
    public void setMorphBall(bool mb)
    {
        this.hasMorphball = mb;
    }

    public int getHighscore()
    {
        return this.highscore;
    }
    public void setHighscore(int hs)
    {
        this.highscore = hs;
    }

}

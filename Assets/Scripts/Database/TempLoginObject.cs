using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLoginObject
{
    bool success;
    string username;

    public TempLoginObject()
    {
        this.username = "";
        this.success = false;
    }

    public bool getSuccess()
    {
        return this.success;
    }
    public void setSuccess(bool res)
    {
        this.success = res;
    }

    public string getUsername()
    {
        return this.username;
    }
    public void setUsername(string input)
    {
        this.username = input;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Saves the object which is created in the SavedValues object
//This is just the stand-between function which actually uses the binary serialization
//It is intended to save the file in a persistent location, named the given username.txt
//For example: testuser.txt
//information used originated from: https://www.youtube.com/watch?v=XOjd_qU2Ido
public static class Saving
{
    public static void SavePlayer(PlayerMovement player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + player.username + ".txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavedValues info = new SavedValues(player);

        formatter.Serialize(stream, info);
        stream.Close();
    }

    public static SavedValues LoadPlayer(string player)
    {
        string path = Application.persistentDataPath + "/" + player + ".txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedValues data = formatter.Deserialize(stream) as SavedValues;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save not found in " + path);
            return null;
        }
    }
}
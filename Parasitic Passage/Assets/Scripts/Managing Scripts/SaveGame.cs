using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

[Serializable]
public class SaveGame : ISerializable
{
    public int levelName;

    //Store the level the player is on
    public void StoreData(Manager model)
    {
        levelName = Application.loadedLevel;
    }

    //Get level
    public void LoadData(Manager model)
    {
        Application.LoadLevel(levelName);
    }

    // Get the level
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("level", levelName);
    }

    // Empty constructor
    public SaveGame()
    {
    }

    // Get info from stream
    public SaveGame(SerializationInfo info, StreamingContext context)
    {
        levelName = info.GetInt32("level");
    }

}
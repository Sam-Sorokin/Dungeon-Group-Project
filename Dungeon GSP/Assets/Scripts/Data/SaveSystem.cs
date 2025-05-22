using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(playerVariables playerVariables)
    {
        playerVariables.save();
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = 
    }
}

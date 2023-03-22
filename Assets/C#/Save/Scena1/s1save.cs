using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class s1save
{

    public static void SavePlayer (s1 player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scenaprincipala.fuc";
        FileStream stream = new FileStream(path , FileMode.Create);
        s1data data = new s1data(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static s1data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/scenaprincipala.fuc";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            s1data data = formatter.Deserialize(stream) as s1data;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }


}

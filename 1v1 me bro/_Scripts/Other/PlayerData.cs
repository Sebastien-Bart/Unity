using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public static int nbBrocoins;
    public static bool hasFullAccess;

    public int savedBrocoins;
    public bool savedHasFullAccess;

    private PlayerData()
    {
        savedBrocoins = nbBrocoins;
        savedHasFullAccess = hasFullAccess;
    }

    private static readonly string fileName = "/brocoinsStuff.wtf";

    public static void SaveBrocoinsAndAccess()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();
        formatter.Serialize(stream, data);
        stream.Close();
        BrocoinsTxt.needUpdate = true;
    }

    public static void LoadBrocoinsAndAccess()
    {
        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            // update
            nbBrocoins = data.savedBrocoins;
            hasFullAccess = data.savedHasFullAccess;
        }
        else
        {
            Debug.LogError("save file \"" + fileName + "\" not found in path: " + path);
            nbBrocoins = 0;
            hasFullAccess = false;
        }
        BrocoinsTxt.needUpdate = true;
    }

}

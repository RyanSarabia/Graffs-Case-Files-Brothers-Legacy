using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(int level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        Debug.Log("Save file at:" + Application.persistentDataPath);

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            LevelData data = new LevelData(level);
            formatter.Serialize(stream, data);
        }    
    }

    public static LevelData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/save.data";
        LevelData data = null;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {                 
                data = formatter.Deserialize(stream) as LevelData;
            }

            return data;
        }
        else
        {
            Debug.Log("Save not found at" + path);
            return null;
        }
       
    }
    public static void deleteSave()
    {
        string path = Application.persistentDataPath + "/save.data";
        File.Delete(path);
        Debug.Log("deleted file at " + path);
    }

    public static bool checkSave()
    {
        string path = Application.persistentDataPath + "/save.data";

        return File.Exists(path);
    }
}

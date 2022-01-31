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
    public static void SaveInstruct(int level, bool state)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/instructions.data";
        //Debug.Log("Save file at:" + Application.persistentDataPath);

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            Debug.Log(stream);
            //Debug.Log("In SAVE: check mark" + state + " lvl" + level);
            InstructionsData data = new(level, state);
            
            formatter.Serialize(stream, data);
        }
    }

    public static InstructionsData LoadInstruct()
    {
        string path = Application.persistentDataPath + "/instructions.data";
        InstructionsData data = null;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as InstructionsData;
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
        string path2 = Application.persistentDataPath + "/instructions.data";
        PlayerPrefs.DeleteAll();
        File.Delete(path);
        File.Delete(path2);
        Debug.Log("deleted file at " + path);
    }

    public static bool checkSave()
    {
        string path = Application.persistentDataPath + "/save.data";

        return File.Exists(path);
    }
}

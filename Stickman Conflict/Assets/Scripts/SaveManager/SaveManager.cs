using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData saveData;
    public bool isDataLoaded;

    private void Awake()
    {
        instance = this;
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Delete();
        }
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/StickMan.DK";
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, saveData);
        stream.Close();
        Debug.Log("Data Save");
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/StickMan.DK";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            saveData = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Data Load");
            isDataLoaded = true;
        }
        else
        {
            Debug.Log("Data not found");
        }
    }

    public void Delete()
    {
        string path = Application.persistentDataPath + "/StickMan.DK";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Data Delete");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public float[] playerSpwanPos = new float[3];
    public float[,] weaponsPosition;
    public bool isWeaponPicked;
    public string pickedWeaponName;
    public float currHealth;
}
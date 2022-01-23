using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance = null;
    public static GameSaveManager Instance
    {
        get { return instance; }
    }

    public SaveData saveData;
    public bool isDataLoaded;

    private void Awake()
    {
        // MakeSingleton();
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

    private void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/StickMan.DK";
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, saveData);
        stream.Close();
        // Debug.Log("Data Save");
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
            isDataLoaded = true;
            // Debug.Log("Data Load");
        }
        else
        {
            //  Debug.Log("Data not found");
        }
    }

    public void Delete()
    {
        string path = Application.persistentDataPath + "/StickMan.DK";
        if (File.Exists(path))
        {
            File.Delete(path);
            //  Debug.Log("Data Delete");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public float[] playerSpwanPos = new float[2];
    public float[,] weaponsPosition = new float[2, 2];
    public string pickedWeaponName;
    public float currHealth;
    public int coin;
    public byte key;
    public byte level;
    public byte palakCount;
    public byte bomb;
}
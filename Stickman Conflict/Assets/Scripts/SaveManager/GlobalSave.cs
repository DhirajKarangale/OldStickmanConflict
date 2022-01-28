using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GlobalSave : MonoBehaviour
{
    public static GlobalSave instance;

    public bool isDataLoaded;
    public GlobalData globalData;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        Load();
        MakeSingleton();
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
        string path = Application.persistentDataPath + "/StickManConflictGlobal.DK";
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, globalData);
        stream.Close();
        // Debug.Log("Game Data Save");
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/StickManConflictGlobal.DK";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            globalData = binaryFormatter.Deserialize(stream) as GlobalData;
            stream.Close();
            isDataLoaded = true;
            // Debug.Log("Game Data Load");
        }
    }

    public void Delete()
    {
        string path = Application.persistentDataPath + "/StickManConflictGlobal.DK";
        if (File.Exists(path))
        {
            File.Delete(path);
            //  Debug.Log("Data Game Delete");
        }
    }
}
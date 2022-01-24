using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameSave : MonoBehaviour
{
    public static GameSave instance;

    public bool isDataLoaded;
    public GameData gameData;

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
        string path = Application.persistentDataPath + "/StickManConflictGame.DK";
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, gameData);
        stream.Close();
        // Debug.Log("Game Data Save");
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/StickManConflictGame.DK";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            gameData = binaryFormatter.Deserialize(stream) as GameData;
            stream.Close();
            isDataLoaded = true;
            // Debug.Log("Game Data Load");
        }
    }

    public void Delete()
    {
        string path = Application.persistentDataPath + "/StickManConflictGame.DK";
        if (File.Exists(path))
        {
            File.Delete(path);
            //  Debug.Log("Data Game Delete");
        }
    }
}
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    // public static SaveManager Instance
    // {
    //     get
    //     {
    //         return instance;
    //     }
    // }

    public SaveData saveData;
    public bool isDataLoaded;
    [SerializeField] GameObject firstTalk;

    private void Awake()
    {
        // if (instance != null && instance != this)
        // {
        //     Destroy(this.gameObject);
        //     return;
        // }
        // else
        // {
        //     instance = this;
        // }
        // DontDestroyOnLoad(this.gameObject);

        instance = this;
        Load();
        if (PlayerPrefs.GetInt("FirstTalk") == 1) Destroy(firstTalk);
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
    public float[][] weaponsPos = new float[1][];
    public bool isWeaponPicked;
    public string pickedWeaponName;
    public float currHealth;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;


    private void Awake()
    {
        if (DataManager.instance == null)
        {
            DataManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SaveData(string filename, object data)
    {
        //C:/Users/PauAltet/AppData/LocalLow/DefaultCompany/burble\users.dat

        string path = Path.Combine(Application.persistentDataPath, filename + ".dat");
        Debug.Log(path);
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public List<UserData> ReadUserDataCollection(string filename)
    {
        string path = Path.Combine(Application.persistentDataPath, filename + ".dat");
        if (!File.Exists(path))
        {
            Debug.Log(filename + " does not exist. Path: " + path);
            return new List<UserData>();
        } else
        {
            string data = File.ReadAllText(path);
            return JsonUtility.FromJson<UserDataCollection>(data).users;
        }
    }

    public UserData GetUserData(string filename, string username)
    {
        List<UserData> users = ReadUserDataCollection(filename);
        foreach(UserData user in users)
        {
            if (user.name == username) return user;
        }
        return null;
    }
}

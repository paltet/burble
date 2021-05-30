using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private string file = "users";

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

    public void AddGameDataToUser(string filename, string username, GameData data)
    {
        List<UserData> users = ReadUserDataCollection(filename);

        UserData udata = null;
        List<UserData> tempUsers = new List<UserData>(users);

        foreach (UserData user in tempUsers)
        {
            if (user.name == username)
            {
                udata = user;
                users.Remove(user);
            }
        }
        if (udata == null)
        {
            Debug.Log("User not found. User: " + username);
            return;
        }

        udata.record.Add(data);
        users.Add(udata);

        UserDataCollection save = new UserDataCollection();
        save.users = users;
        SaveData(filename, save);
    }

    public bool GenerateReport(string filename, string username)
    {
        UserData data = GetUserData(filename, username);

        if (data == null)
        {
            Debug.Log("User not found. User: " + filename);
            return false;
        }

        List<GameData> gamedata = data.record;
        string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        path = path + "/" + username + ".csv";
        Debug.Log(path);
        string separador = ",";
        StringBuilder builder = new StringBuilder();

        string header = "user,date,prism,score";

        builder.AppendLine(string.Join(separador, header));

        foreach(GameData game in gamedata)
        {
            string cadena = username + separador + game.date + separador + game.prisms.ToString() + separador + game.score.ToString();
            builder.AppendLine(string.Join(separador, cadena));
        }

        File.WriteAllText(path, builder.ToString());

        if (File.Exists(path)) return true;
        return false;
    }

    public int GetMaxScoreSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;

        int max = 0;

        foreach(GameData game in gamedata)
        {
            if (game.score > max) max = game.score;
        }

        return max;
    }

    public int GetTotalScoreSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;

        int total = 0;

        foreach (GameData game in gamedata)
        {
            total += game.score;
        }

        return total;
    }

    public int GetTotalGamesUserSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        return data.record.Count;
    }

    public int GetTotalDaysUserSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;
        HashSet<string> days = new HashSet<string>();

        foreach(GameData game in gamedata){
            string day = game.date.Substring(0, 10); //dd/mm/yyyy (10 chars)
            days.Add(day);
        }

        return days.Count;
    }

    public int GetMaxTeethSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;

        int max = 0;

        foreach (GameData game in gamedata)
        {
            if (game.teeth > max) max = game.teeth;
        }

        return max;
    }

    public int GetTotalTeethSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;

        int total = 0;

        foreach (GameData game in gamedata)
        {
            total += game.teeth;
        }

        return total;
    }

    public int GetMaxKilledFishSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;

        int max = 0;

        foreach (GameData game in gamedata)
        {
            if (game.teeth > max) max = game.killedfish;
        }

        return max;
    }

    public int GetTotalKilledFishSession()
    {
        UserData data = GetUserData(file, AppManager.instance.GetSessionName());
        List<GameData> gamedata = data.record;

        int total = 0;

        foreach (GameData game in gamedata)
        {
            total += game.killedfish;
        }

        return total;
    }

    public void AddAchievementToUserSession(string achievement)
    {
        List<UserData> users = ReadUserDataCollection(file);

        UserData udata = null;
        List<UserData> tempUsers = new List<UserData>(users);
        string username = AppManager.instance.GetSessionName();

        foreach (UserData user in tempUsers)
        {
            if (user.name == username)
            {
                udata = user;
                users.Remove(user);
            }
        }
        if (udata == null)
        {
            Debug.Log("User not found. User: " + username);
            return;
        }

        udata.achievements.Add(achievement);
        users.Add(udata);

        UserDataCollection save = new UserDataCollection();
        save.users = users;
        SaveData(file, save);
    }

    public bool UserSessionHasAchievement(string achievement)
    {
        UserData udata = GetUserData(file, AppManager.instance.GetSessionName());
        return udata.achievements.Contains(achievement);
    }


}

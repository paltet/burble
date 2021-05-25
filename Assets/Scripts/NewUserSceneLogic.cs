using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class NewUserSceneLogic : MonoBehaviour
{
    TMP_InputField input_name;
    UserDataCollection collection;
    List<UserData> list;

    public void Start()
    {
        input_name = GetComponentInChildren<TMP_InputField>();
        list = DataManager.instance.ReadUserDataCollection("users");
        collection = new UserDataCollection();

        if (list.Count == 0)
        {
            list = new List<UserData>();
        }
    }

    public void CreateUser()
    {
        if (input_name.text == null || input_name.text == "")
        {
            Debug.Log("empty name");
        }
        else
        {
            UserData data = new UserData();
            data.name = input_name.text;
            data.record = new List<GameData>();

            list.Add(data);
            collection.users = list;

            DataManager.instance.SaveData("users", collection);
            if (AppManager.instance.ChangeSession(data.name))
            {
                AppManager.instance.LoadScene("main");
            }
        }
    }

    public void Return()
    {
        AppManager.instance.LoadScene("user");
    }
}

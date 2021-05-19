using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;
    private UserData session;


    private void Awake()
    {
        if (AppManager.instance == null)
        {
            AppManager.instance = this;
            DontDestroyOnLoad(gameObject);
            session = null;
        }
        else Destroy(gameObject);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    
    public bool ChangeSession(string name)
    {
        UserData newuser = DataManager.instance.GetUserData("users", name);
        if (newuser == null)
        {
            Debug.Log("Trying to change session to non-existing user: " + name);
            return false;
        } else
        {
            session = newuser;
            Debug.Log("Changing session to user: " + session.name);
            return true;
        }
    }

    public string GetSessionName()
    {
        if (session == null) return "null";
        else return session.name;
    }
}

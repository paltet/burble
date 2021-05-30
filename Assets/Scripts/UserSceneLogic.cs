using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserSceneLogic : MonoBehaviour
{
    public GameObject UserButtonPrefab;

    public void Awake()
    {
        UpdateUsersSelector();
        AppManager.instance.PlayAudio("ocean");
    }

    public void LoadScene(string name)
    {
        AppManager.instance.LoadScene(name);
    }

    public void UpdateUsersSelector()
    {
        List<UserData> users = DataManager.instance.ReadUserDataCollection("users");
        GameObject userselector = GameObject.Find("UserSelector");

        if (users.Count == 0)
        {
            userselector.transform.Find("ScrollSelectUser").gameObject.SetActive(false);
        }
        else
        {
            userselector.transform.Find("NoUsersText").gameObject.SetActive(false);

            Transform parent = userselector.transform.Find("ScrollSelectUser").transform.Find("Panel").transform;

            foreach (UserData user in users)
            {
                GameObject button = Instantiate(UserButtonPrefab);
                button.transform.parent = parent;
                button.transform.localScale = new Vector3(1f, 1f, 1f);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = user.name;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}

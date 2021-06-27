using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainSceneLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeUserLabel();
        AppManager.instance.PlayAudio("ocean");
    }

    void ChangeUserLabel()
    {
        Text text = transform.Find("UserLabel").GetComponentInChildren<Text>();
        text.text = AppManager.instance.GetSessionName();
    }

    public void Play()
    {
        AppManager.instance.LoadScene("game");
    }

    public void Return()
    {
        AppManager.instance.ClearSession();
        AppManager.instance.LoadScene("user");
    }

    public void LoadScene(string name)
    {
        AppManager.instance.LoadScene(name);
    }
}

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
}

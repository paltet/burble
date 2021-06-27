using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonLogic : MonoBehaviour
{
    public Sprite muted;
    public Sprite unmuted;
    private Image image;

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        if (AppManager.instance.muted)
        {
            image.sprite = muted;
            AudioListener.volume = 0;
        }
        else
        {
            image.sprite = unmuted;
            AudioListener.volume = 1;
        }
    }

    public void ChangeAudio()
    {
        AppManager.instance.muted = !AppManager.instance.muted;

        if (AppManager.instance.muted)
        {
            image.sprite = muted;
            AudioListener.volume = 0;
        }
        else
        {
            image.sprite = unmuted;
            AudioListener.volume = 1;
        }
    }
}

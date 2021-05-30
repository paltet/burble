using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;
    private string session;

    private Animator transition;
    public GameObject transitionCanvas;
    public Transform audioContainer;

    public bool muted = false;

    private void Awake()
    {
        if (AppManager.instance == null)
        {
            AppManager.instance = this;
            DontDestroyOnLoad(gameObject);
            session = null;
        }
        else Destroy(gameObject);

        transition = GetComponentInChildren<Animator>();
        audioContainer = transform.Find("AudioContainer");
    }

    public void LoadScene(string name)
    {
        StartCoroutine(ChargeScene(name));
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
            session = newuser.name;
            Debug.Log("Changing session to user: " + session);
            return true;
        }
    }

    public string GetSessionName()
    {
        if (session == null) return "null";
        else return session;
    }

    public void AddGameData(GameData data)
    {
        DataManager.instance.AddGameDataToUser("users", session, data);
    }

    public void ClearSession()
    {
        session = null;
    }

    IEnumerator ChargeScene(string name)
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        ReloadCanvas();
        SceneManager.LoadScene(name);
    }

    void ReloadCanvas()
    {
        Transform oldcanvasTransform;
        oldcanvasTransform = transform.Find("TransitionCanvas");
        if (oldcanvasTransform == null) oldcanvasTransform = transform.Find("TransitionCanvas(Clone)");

        GameObject oldcanvas = oldcanvasTransform.gameObject;

        GameObject newcanvas = Instantiate(transitionCanvas);
        newcanvas.transform.position = oldcanvas.transform.position;
        newcanvas.transform.parent = oldcanvas.transform.parent;
        Destroy(oldcanvas);
        transition = newcanvas.transform.GetComponentInChildren<Animator>();
    }

    public void PlayAudio(string name)
    {
        GameObject source = audioContainer.Find(name).gameObject;
        if (source == null)
        {
            Debug.Log("Audio " + name + " not found.");
            return;
        }
        AudioSource audio = source.GetComponent<AudioSource>();
        if (audio == null)
        {
            Debug.Log("Audio " + name + " not found.");
            return;
        }

        Debug.Log("sound " + name);
        audio.Play();
    }

    public void StopAllAudio()
    {
        for (int i = 0; i < audioContainer.childCount; i++)
        {
            AudioSource audio = audioContainer.GetChild(i).gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Stop();
            }
        }
    }
}

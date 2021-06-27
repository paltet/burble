using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectiblesSceneLogic : MonoBehaviour
{
    public Sprite verysad, sad, neutral, happy, veryhappy;
    private int nTeeth;

    public void Awake()
    {
        nTeeth = DataManager.instance.GetTotalTeethSession();
        UpdateFace();

        if (DataManager.instance.UserSessionHasSeenTutorial("collectibles"))
        {
            GameObject tutorial = GameObject.Find("Tutorial");
            if (tutorial != null) tutorial.SetActive(false);
        }

        if (nTeeth >= 20) StartCoroutine(Yell());
    }

    public void LoadScene(string name)
    {
        CancelInvoke();
        AppManager.instance.LoadScene(name);
    }
    private void UpdateFace()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject teeth = transform.Find("teeth" + i).gameObject;
            if (teeth != null){
                if (i > nTeeth-1)
                {
                    teeth.SetActive(false);
                }
            }
        }

        GameObject eyes = transform.Find("Eyes").gameObject;
        if (eyes != null)
        {
            Image image = eyes.GetComponent<Image>();
            if (image != null)
            {
                if (nTeeth >= 10) image.sprite = veryhappy;
                else if (nTeeth >= 7) image.sprite = happy;
                else if (nTeeth >= 4) image.sprite = neutral;
                else if (nTeeth >= 1) image.sprite = sad;
                else image.sprite = verysad;
            }
        }
    }

    public void CompleteTutorial()
    {
        GameObject tutorial = GameObject.Find("Tutorial");
        if (tutorial != null) tutorial.SetActive(false);
        DataManager.instance.UserSessionCompleteTutorial("collectibles");
    }

    private IEnumerator Yell()
    {
        yield return new WaitForSeconds(1.0f);
        AppManager.instance.PlayAudio("yay");
    }
}

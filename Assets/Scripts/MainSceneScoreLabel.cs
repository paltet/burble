using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainSceneScoreLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        int max = DataManager.instance.GetMaxScoreSession();
        int total = DataManager.instance.GetTotalScoreSession();
        text.text = "Puntuació Màxima: " + max + "\n" + "Puntuació Total: " + total;
    }
}

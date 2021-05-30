using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSlider : MonoBehaviour
{
    GameManager gm;
    Slider sl;
    TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        gm = Camera.main.GetComponent<GameManager>();
        sl = GetComponent<Slider>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        RefreshAll();
    }
    
    public void Refresh()
    {
        sl.value = gm.score - gm.score_previous;
    }

    public void RefreshAll()
    {
        text.text = gm.scorelevel.ToString();
        sl.minValue = gm.score_previous;
        sl.value = gm.score - gm.score_previous;
        sl.maxValue = gm.score_next_level;
    }
}

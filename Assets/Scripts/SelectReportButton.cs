using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectReportButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Selected()
    {
        bool res = DataManager.instance.GenerateReport("users", gameObject.GetComponentInChildren<TMP_Text>().text);
        GameObject.Find("Canvas").GetComponent<ReportsSceneLogic>().ReportGenerated(res);
    }
}

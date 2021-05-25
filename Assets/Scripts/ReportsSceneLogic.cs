using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReportsSceneLogic : MonoBehaviour
{
    public GameObject ReportButtonPrefab;
    public TMP_Text result;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateReportsSelector();
        result = transform.Find("ResultText").gameObject.GetComponent<TMP_Text>();
        result.gameObject.SetActive(false);
    }

    void UpdateReportsSelector()
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
                GameObject button = Instantiate(ReportButtonPrefab);
                button.transform.parent = parent;
                button.transform.localScale = new Vector3(1f, 1f, 1f);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = user.name;
            }
        }
    }

    public void Return()
    {
        AppManager.instance.LoadScene("user");
    }

    public void ReportGenerated(bool res)
    {
        string ok = "El report s'ha generat correctament.";
        string ko = "Hi ha hagut algún error, no s'ha pogut crear.";

        result.gameObject.SetActive(true);

        if (res) result.text = ok;
        else result.text = ko;
    }
}

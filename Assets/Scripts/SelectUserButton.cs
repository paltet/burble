using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectUserButton : MonoBehaviour
{
    // Start is called before the first frame update

    public void Selected()
    {
        if (AppManager.instance.ChangeSession(gameObject.GetComponentInChildren<TMP_Text>().text))
        {
            AppManager.instance.LoadScene("main");
        }
    }
}

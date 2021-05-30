using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesSceneLogic : MonoBehaviour
{
    public GameObject[] objectives;

    public GameObject objPrefab;
    private GameObject container;

    public void Awake()
    {
        container = GameObject.Find("Panel");
        if (container == null) return;

        foreach (GameObject obj in objectives)
        {
            ObjDataToPanel(obj.GetComponent<ObjectiveData>());
        }
    }

    public void Return()
    {
        AppManager.instance.LoadScene("main");
    }

    public void ObjDataToPanel(ObjectiveData data)
    {
        GameObject ret = Instantiate(objPrefab);
        ret.transform.parent = container.transform;
        ret.GetComponent<ObjectiveLogic>().Set(data);
    }
}

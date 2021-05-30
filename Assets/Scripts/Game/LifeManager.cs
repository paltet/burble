using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    private CameraShaker sh;

    // Start is called before the first frame update
    void Start()
    {
        sh = Camera.main.GetComponent<CameraShaker>();
        Debug.Log(sh.isActiveAndEnabled);
    }

    public void LoseLife(int lifes)
    {
        //Debug.Log(lifes);
        if (lifes == 0) gameObject.SetActive(false);
        else
        {
            for (int i = 1; i <= transform.childCount; i++)
            {
                if (i > lifes)
                {
                    transform.Find("life" + i.ToString()).gameObject.SetActive(false);
                    StartCoroutine(Camera.main.GetComponent<CameraShaker>().Shake(0.1f, 0.1f));
                }
            }
        }
    }
}

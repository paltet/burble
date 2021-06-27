using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResults : MonoBehaviour
{
    AnaglyphEffect ae;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        ae = Camera.main.GetComponent<AnaglyphEffect>();

        Vector3 inv = new Vector3(0f, 0f, 0f);
        Vector3 outv = new Vector3(1f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

        }
    }
}

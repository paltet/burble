using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{

    private Vector3 startpos;
    private float y_offset = 10f;
    private float x_offset = 50f;
    private float y_max_movement = 0.5f;
    private float x_max_movement = 0.25f;
    private float scale_rate = 0.01f;

    void Start()
    {
        startpos = transform.localPosition;
        float startscale = transform.localScale.x;
        float scale = startscale - startpos.y * scale_rate;
        transform.localScale = new Vector3(scale, scale, 1);
        //startpos.y = startpos.y - startpos.y * 0.25f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = startpos + new Vector3(Mathf.Sin(x_offset * startpos.y + Time.time) * x_max_movement, Mathf.Sin(startpos.y * y_offset + Time.time) * y_max_movement, 0);
    }
}

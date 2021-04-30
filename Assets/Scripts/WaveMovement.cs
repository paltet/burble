using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{

    private Vector3 startpos;
    public float y_offset = 10f;
    public float x_offset = 0.25f;
    public float y_max_movement = 0.5f;
    public float x_max_movement = 0.25f;

    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startpos + new Vector3(Mathf.Sin(x_offset * startpos.y + Time.time) * x_max_movement, Mathf.Sin(startpos.y * y_offset + Time.time) * y_max_movement, 0); 
    }
}

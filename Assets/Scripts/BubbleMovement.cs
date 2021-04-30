using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BubbleManager))]
public class BubbleMovement : MonoBehaviour
{
    public Vector3 startScale;
    public float maxscale = 1f;
    private float timeoffset;
    public float scalespeed = 1f;

    public float floatspeed = 1f;
    private Rigidbody rb;

    private void Start()
    {
        startScale = transform.localScale;
        maxscale /= 2;
        timeoffset = Random.Range(0f, 200f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<BubbleManager>().popped)
        {
            rb.useGravity = true;
        }
        else
        {
            transform.localScale = startScale + new Vector3(Mathf.Sin((Time.time + timeoffset) * scalespeed), Mathf.Sin((Time.time + timeoffset) * scalespeed), 0.0f) * maxscale;
            transform.position = new Vector3(transform.position.x, transform.position.y + floatspeed * 0.005f, transform.position.z);

            rb.velocity *= 0.99f;

            if (Input.GetKeyDown("left"))
            {
                rb.AddForce(-5, -1, 0, ForceMode.Impulse);
            }
        }
    }
}

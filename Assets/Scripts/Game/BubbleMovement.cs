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

    public float floatspeed;
    private Rigidbody2D rb;

    float initialspeed;

    private void Start()
    {
        startScale = transform.localScale;
        maxscale /= 2;
        timeoffset = Random.Range(0f, 200f);
        rb = GetComponent<Rigidbody2D>();

        float height = Camera.main.orthographicSize * 2;
        initialspeed = height/10;
        floatspeed = initialspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<BubbleManager>().popped)
        {
            rb.gravityScale = 1;
        }
        else
        {
            transform.localScale = startScale + new Vector3(Mathf.Sin((Time.time + timeoffset) * scalespeed), Mathf.Sin((Time.time + timeoffset) * scalespeed), 0.0f) * maxscale;
            transform.Translate(0, floatspeed * Time.deltaTime, 0);

            rb.velocity *= 0.99f;
        }
    }
}

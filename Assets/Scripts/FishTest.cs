using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTest : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite[] sprites;
    private int sprite = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            sprite = (sprite + 1) % sprites.Length;
            sr.sprite = sprites[sprite];
        }
    }
}

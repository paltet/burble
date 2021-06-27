using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollision : MonoBehaviour
{
    bool inside;
    FishManager fm;

    // Update is called once per frame
    void Start()
    {
        if (gameObject.tag == "Inside") inside = true;
        else inside = false;

        fm = GetComponentInParent<FishManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inside) fm.TriggeredInside(collision.gameObject);
        else fm.TriggeredOutside(collision.gameObject);
    }
}

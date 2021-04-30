using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Vertical");
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + h * speed);
    }
}

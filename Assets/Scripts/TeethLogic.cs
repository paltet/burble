using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethLogic : MonoBehaviour
{
    Vector2 startingPos;

    void Awake()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    }

    
}

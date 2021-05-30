using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveData : MonoBehaviour
{
    public enum Element
    {
        Games,
        Score,
        Teeth,
        Days,
        KilledFish
    }
    public enum Reference
    {
        SingleGame,
        AllUserGames
    }

    public string id;
    public Element element;
    public Reference reference;
    public int magnitude;
    public string reward;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

[Serializable]
public class GameData
{
    public string date;
    public float prisms;
    public int score;
    public int teeth;
    public int killedfish;
}

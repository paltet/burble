using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

[Serializable]
public class UserData
{
    public string name;
    public List<GameData> record;
}

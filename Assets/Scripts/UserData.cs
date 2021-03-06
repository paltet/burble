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
    public List<string> achievements;
    public bool gametutorial;
    public bool achievementstutorial;
    public bool collectiblestutorial;
}

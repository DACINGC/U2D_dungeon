using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public int currence;
    public List<string> equipmentId;
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, float> volumeSlider;

    public SerializableDictionary<string, bool> checkPoints;
    public string LastCheckPointId;

    public GameData()
    { 
        currence = 0;
        equipmentId = new List<string>();
        inventory = new SerializableDictionary<string, int>();
        skillTree = new SerializableDictionary<string, bool>();
        volumeSlider = new SerializableDictionary<string, float>();
        checkPoints = new SerializableDictionary<string, bool>();
        LastCheckPointId = string.Empty;
    }
}

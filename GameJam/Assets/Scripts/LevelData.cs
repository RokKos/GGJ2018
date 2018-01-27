using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class LevelData : ScriptableObject {
    public string TextFromAudio = "Test";
    public int CorrectCordinateX = -1;
    public int CorrectCordinateY = -1;

    public string WinText = "";
    public string LoseText = "";

    public bool AutoComplete = true;
}

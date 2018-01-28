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

    public int IndexOfImage = 0;

    public List<Vector2> TankCord;
    public List<Vector2> PlaneCord;
    public List<Vector2> SubMarineCord;

    public bool AutoComplete = true;
    public bool FreeRoamMode = false;
}

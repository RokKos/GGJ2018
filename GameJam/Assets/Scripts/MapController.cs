using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [SerializeField] CordinateButtonController PrefabButton;
    [SerializeField] GameController GameController;

    private List<CordinateButtonController> AllCordinates = new List<CordinateButtonController>();

    const int LeftMostSide = -511;
    const int UpMostSide = 197;
    const int SizeOfButton = 145;
    // Use this for initialization
    void Start () {
        for (int y = 1; y < 5; ++y) {
            for (int x = 1; x < 9; ++x) {
                CordinateButtonController obj = Instantiate(PrefabButton, this.transform);
                AllCordinates.Add(obj);
                obj.transform.localPosition = new Vector3(LeftMostSide + (x - 1) * SizeOfButton, UpMostSide - (y - 1) * SizeOfButton);
                obj.SetterOfCordinate(x, y);
                obj.name = "Cord: X:" + x.ToString() + " Y:" + y.ToString();
                obj.GameController = GameController;
                obj.Image.enabled = false;
            }
        }
	}

    public void ClearAllCordinates () {
        for (int i = 0; i < AllCordinates.Count; ++i) {
            AllCordinates[i].Image.enabled = false;
            AllCordinates[i].Image.color = Color.white;
        }
    }
}

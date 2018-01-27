using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [SerializeField] CordinateButtonController PrefabButton;
    [SerializeField] GameController GameController;

    const int LeftMostSide = -511;
    const int UpMostSide = 197;
    const int SizeOfButton = 150;
    // Use this for initialization
    void Start () {
        for (int y = 1; y < 5; ++y) {
            for (int x = 1; x < 9; ++x) {
                CordinateButtonController obj = Instantiate(PrefabButton, this.transform);
                obj.transform.localPosition = new Vector3(LeftMostSide + (x - 1) * SizeOfButton, UpMostSide - (y - 1) * SizeOfButton);
                obj.SetterOfCordinate(x, y);
                obj.name = "Cord: X:" + x.ToString() + " Y:" + y.ToString();
                obj.GameController = GameController;
                obj.Image.enabled = false;
            }
        }
	}
}

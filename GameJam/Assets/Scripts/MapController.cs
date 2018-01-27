using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [SerializeField] CordinateButtonController PrefabButton;
    [SerializeField] GameController GameController;

    const int LeftMostSide = -450;
    const int UpMostSide = 200;
    // Use this for initialization
    void Start () {
        for (int y = 1; y < 11; ++y) {
            for (int x = 1; x < 11; ++x) {
                CordinateButtonController obj = Instantiate(PrefabButton, this.transform);
                obj.transform.localPosition = new Vector3(LeftMostSide + x * 50, UpMostSide - y * 50);
                obj.SetterOfCordinate(x, y);
                obj.name = "Cord: X:" + x.ToString() + " Y:" + y.ToString();
                obj.GameController = GameController;

            }
        }
	}
}

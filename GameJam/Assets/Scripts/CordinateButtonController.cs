using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CordinateButtonController : MonoBehaviour {

    [SerializeField] Button Button;
    [SerializeField] Text TextCordinate;

    public GameController GameController;

    private int CordinateX = -1;
    private int CordinateY = -1;

    // Use this for initialization
    void Start () {
        Button.onClick.AddListener(RecordCordinate);
    }

    void RecordCordinate () {
        Debug.Log("You have clicked the button!");
        Debug.Log(CordinateX + " " + CordinateY);
        GameController.GetPlayerAction(CordinateX, CordinateY);
    }

    public void SetterOfCordinate (int x, int y) {
        CordinateX = x;
        CordinateY = y;
        TextCordinate.text = x.ToString() + "," + y.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CordinateButtonController : MonoBehaviour {

    [SerializeField] Button Button;
    [SerializeField] Text TextCordinate;
    public Image Image;

    public GameController GameController;

    private const float AnimationTime = 1.0f;
    private float CurrentTime = 0.0f;
    private bool StartedAnimation = false;

    private int CordinateX = -1;
    private int CordinateY = -1;

    // Use this for initialization
    void Start () {
        Button.onClick.AddListener(RecordCordinate);
    }

    private void Update () {
        if (!StartedAnimation) {
            return;
        }

        CurrentTime += Time.deltaTime;
        if (CurrentTime > AnimationTime) {
            GameController.GetPlayerAction(CordinateX, CordinateY);
            StartedAnimation = false;
        }
    }

    void RecordCordinate () {
        Debug.Log("You have clicked the button!");
        Debug.Log(CordinateX + " " + CordinateY);
        Image.enabled = true;
        Image.color = Color.red;

        // Start Animation
        StartedAnimation = true;
        CurrentTime = 0.0f;
    }

    public void SetterOfCordinate (int x, int y) {
        CordinateX = x;
        CordinateY = y;
        TextCordinate.text = x.ToString() + "," + y.ToString();
    }
}

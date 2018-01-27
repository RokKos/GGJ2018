using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour {

    public float timeBetweenLetters = 1.5f;
    public float time = 0.2f;

    float keyDownTime;
    float keyUpTime;
    Text morseText;

	// Use this for initialization
	void Start () {
        morseText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            KeyDown();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            KeyUp();
        }
        if (Input.GetKeyUp(KeyCode.Backspace)) {
            RemoveLetter();
        }
    }

    void KeyDown()
    {
        keyDownTime = Time.time;
    }

    void KeyUp()
    {
        float lastKeyUpTime = keyUpTime;
        if(Time.time - lastKeyUpTime >= timeBetweenLetters) {
            morseText.text += " / ";
        }
        keyUpTime = Time.time;
        string signal = "";
        if(keyUpTime - keyDownTime <= time) {
            signal = ".";
        } else {
            signal = "-";
        }
        morseText.text += signal;
    }

    void RemoveLetter()
    {
        if (morseText.text.Length > 0) {
            morseText.text = morseText.text.Substring(0, morseText.text.Length - 1);
        }
    }
}

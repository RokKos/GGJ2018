using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour {

    [SerializeField] Animator rightArmAnimator;

    public float timeBetweenLetters = 1.5f;
    public float time = 0.2f;

    float keyDownTime;
    float keyUpTime;
    Text MorseHolder;

	// Use this for initialization
	void Start () {
        MorseHolder = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            Noise();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            KeyDown();
            rightArmAnimator.SetBool("goDown", true);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            KeyUp();
            rightArmAnimator.SetBool("goDown", false);
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
        if(Time.time - lastKeyUpTime >= timeBetweenLetters && MorseHolder.text != "") {
            MorseHolder.text += " ";
        }
        keyUpTime = Time.time;
        string signal = "";
        if(keyUpTime - keyDownTime <= time) {
            signal = ".";
        } else {
            signal = "-";
        }
        MorseHolder.text += signal;
    }

    void RemoveLetter()
    {
        if (MorseHolder.text.Length > 0) {
            MorseHolder.text = MorseHolder.text.Substring(0, MorseHolder.text.Length - 1);
        }
    }

    void Noise()
    {
        MorseHolder.text += " #";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInputedText : MonoBehaviour {

    [SerializeField] Animator leftArmAnimator;
    Text textElement;

	// Use this for initialization
	void Start () {
        textElement = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            textElement.text += Input.inputString;
            textElement.text = RemoveDubleSpace(textElement.text);
            if(Input.inputString == "q" || Input.inputString == "w" || Input.inputString == "e" || 
                Input.inputString == "a" || Input.inputString == "s" || Input.inputString == "d" || 
                Input.inputString == "y" || Input.inputString == "x" || Input.inputString == "c") {
                leftArmAnimator.SetTrigger("pinky");
            }else if (Input.inputString == "r" || Input.inputString == "t" || Input.inputString == "z" ||
                Input.inputString == "f" || Input.inputString == "g" || Input.inputString == "h" ||
                Input.inputString == "v" || Input.inputString == "b" || Input.inputString == "n") {
                leftArmAnimator.SetTrigger("ring");
            } else if (Input.inputString == "u" || Input.inputString == "i" || Input.inputString == "h" ||
                 Input.inputString == "j" || Input.inputString == "k" || Input.inputString == "m") {
                leftArmAnimator.SetTrigger("middle");
            } else if(Input.inputString != " ") {
                leftArmAnimator.SetTrigger("index");
            }
        }
	}

    string RemoveDubleSpace(string text)
    {
        Debug.Log(text + " length: " + text.Length);
        //if (text[0] == ' ') text = text.Substring(1, text.Length-2);
        string newText = "";
        char charBefore = ' ';
        foreach(char c in text) {
            if (c == ' ' && charBefore == ' ') ;
            else newText += charBefore;
            charBefore = c;
        }
        newText += charBefore;
        return newText;
    }
}

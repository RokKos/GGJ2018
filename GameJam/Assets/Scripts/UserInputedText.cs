using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInputedText : MonoBehaviour {

    Text textElement;

	// Use this for initialization
	void Start () {
        textElement = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            textElement.text += Input.inputString;
        }
	}
}

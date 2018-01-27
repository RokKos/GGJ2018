using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowClickLength : MonoBehaviour {

    Slider slider;


    float startTime = 0;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space)) {
            KeyDown();
            Debug.Log("keydown");
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            KeyUp();
        }
	}

    void KeyDown()
    {
        if (startTime == 0) startTime = Time.time;

        float value = ((Time.time - startTime) / 0.4f)*1000;
        slider.value = value;
    }

    void KeyUp()
    {
        startTime = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject MorseText;
    [SerializeField] GameObject MapImage;

    private bool IsTextActive = true;

    // Use this for initialization
    void Start () {
        EnableScreenObjects();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M)) {
            IsTextActive = !IsTextActive;
            EnableScreenObjects();
        }
	}

    void EnableScreenObjects () {
        MorseText.SetActive(IsTextActive);
        MapImage.SetActive(!IsTextActive);
    }
}

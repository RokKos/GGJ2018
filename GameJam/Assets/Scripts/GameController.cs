using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject MorseText;
    [SerializeField] GameObject MapImage;
    [SerializeField] SoundController SoundController;
    [SerializeField] List<LevelData> AllLevelData;

    private bool IsTextActive = true;

    private int LevelNumber = 0;
    private bool PastGameMenu = false;

    // Use this for initialization
    void Start () {
        EnableScreenObjects();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            PastGameMenu = true;
            StartLevel();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            IsTextActive = !IsTextActive;
            EnableScreenObjects();
        }
	}

    void EnableScreenObjects () {
        MorseText.SetActive(IsTextActive);
        MapImage.SetActive(!IsTextActive);
    }

    void StartLevel () {
        SoundController.PlayLevel(AllLevelData[LevelNumber].TextFromAudio);
        LevelNumber++;
    }

    public bool GetPastGameMenu () {
        return PastGameMenu;
    }

    public void GetPlayerAction (int x, int y) {
        
    }
}

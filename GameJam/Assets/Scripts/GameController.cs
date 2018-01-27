using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject MorseText;
    [SerializeField] GameObject MapImage;
    [SerializeField] GameObject ResolutionHolder;
    [SerializeField] Text ResolutionText;
    [SerializeField] SoundController SoundController;
    [SerializeField] List<LevelData> AllLevelData;

    enum PCScreenState { Morse, Map, Resolution };

    private PCScreenState PCState = PCScreenState.Morse;

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
            if (PCState == PCScreenState.Morse) {
                PCState = PCScreenState.Map;
            } else if (PCState == PCScreenState.Map) {
                PCState = PCScreenState.Morse;
            } 
            EnableScreenObjects();
        }
	}

    void EnableScreenObjects () {
        switch (PCState) {
            case PCScreenState.Morse:
                MorseText.SetActive(true);
                MapImage.SetActive(false);
                ResolutionHolder.SetActive(false);
                break;
            case PCScreenState.Map:
                MorseText.SetActive(false);
                MapImage.SetActive(true);
                ResolutionHolder.SetActive(false);
                break;

            case PCScreenState.Resolution:
                MorseText.SetActive(false);
                MapImage.SetActive(false);
                ResolutionHolder.SetActive(true);
                break;
        }
    }

    void StartLevel () {
        PCState = PCScreenState.Morse;
        EnableScreenObjects();
        SoundController.ResetQueue();
        SoundController.PlayLevel(AllLevelData[LevelNumber].TextFromAudio);
       
    }

    public bool GetPastGameMenu () {
        return PastGameMenu;
    }

    public void GetPlayerAction (int x, int y) {
        

        if (EvaluateAction(x, y)) {
            // TODO: revard player
            ResolutionText.text = AllLevelData[LevelNumber].WinText;
        } else {
            // TODO: penalize player
            ResolutionText.text = AllLevelData[LevelNumber].LoseText;
        }

        PCState = PCScreenState.Resolution;
        EnableScreenObjects();

        LevelNumber++;
    }

    private bool EvaluateAction (int x, int y) {
        return AllLevelData[LevelNumber].CorrectCordinateX == x && AllLevelData[LevelNumber].CorrectCordinateY == y;
    }

    public void NextLevelStart () {
        StartLevel();
    }
}

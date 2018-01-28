using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] Animator rightArmAnimator;
    [SerializeField] GameObject MorseHolder;
    [SerializeField] GameObject MapImage;

    [SerializeField] GameObject ResolutionHolder;
    [SerializeField] Text ResolutionText;
    [SerializeField] SoundController SoundManager;
    [SerializeField] DecodeText DecodeText;
    [SerializeField] UserInputedText UserInputedText;
    [SerializeField] MapController MapController;
    [SerializeField] UserInput UserInput;

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
        if (Input.GetKeyDown(KeyCode.Return) && !SoundManager.SoundPlaying()) {
            PastGameMenu = true;
            StartLevel();
        }

        if (Input.GetKeyDown(KeyCode.F5)) {
            // Cheat
            DecodeText.ChuckNoris(AllLevelData[LevelNumber].TextFromAudio);
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
                MorseHolder.SetActive(true);
                MapImage.SetActive(false);
                ResolutionHolder.SetActive(false);
                break;
            case PCScreenState.Map:
                MorseHolder.SetActive(false);
                MapImage.SetActive(true);
                ResolutionHolder.SetActive(false);
                break;

            case PCScreenState.Resolution:
                MorseHolder.SetActive(false);
                MapImage.SetActive(false);
                ResolutionHolder.SetActive(true);
                break;
        }
    }

    void StartLevel () {
        PCState = PCScreenState.Morse;
        EnableScreenObjects();

        DecodeText.enabled = AllLevelData[LevelNumber].AutoComplete;
        UserInputedText.enabled = !AllLevelData[LevelNumber].AutoComplete;
        if (UserInputedText.enabled) {
            UserInputedText.ResetUserText();
        }
        if (DecodeText.enabled) {
            DecodeText.ResetDecode();
        }

        // Play disk animation at tutorial start


        UserInput.ResetMorseText();
       
        SoundManager.ResetQueue();
        SoundManager.PlayLevel(AllLevelData[LevelNumber].TextFromAudio);
        MapController.SetSpritesOnCordinates();
        MapController.SetMapSprite(AllLevelData[LevelNumber].IndexOfImage);
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
        MapController.ClearAllCordinates();
        StartLevel();
    }

    public LevelData GetLevelData () {
        return AllLevelData[LevelNumber];
    }

    public void ResetTransmition () {
        StartLevel();
    }
}

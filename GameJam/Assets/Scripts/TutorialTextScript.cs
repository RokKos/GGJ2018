using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour {

    [SerializeField] DecodeText decodeText;
    [SerializeField] UserInputedText userInputedText;
    [SerializeField] Text morseText;
    [SerializeField] Text decodedText;
    AudioSource audioSource;

    string tutorialText = "PRESS ENTER TO START TUTORIAL.";
    string tutorialMorseText = "...";
    int nextChar = 0;
    int nextMorseChar = 0;
    const float timeInterval = 0.24f;
    const float timeMorseInterval = 0.12f;
    float nextCharTime = 0;
    float nextMorseCharTime = 0;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        PreLoadTutorialInstructions();
        decodeText.enabled = false;
        userInputedText.enabled = false;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Return)){
            audioSource.Stop();
            morseText.text = "";
            decodedText.text = "";
            Destroy(gameObject);
        }
    }

    void PreLoadTutorialInstructions()
    {
        audioSource.Play();
        tutorialMorseText = TextToMorseCode();

        foreach (char c in tutorialText) {
            nextCharTime += timeInterval;
            Invoke("AutoWrite", nextCharTime);
        }
        foreach (char c in tutorialMorseText) {
            nextMorseCharTime += timeMorseInterval;
            Invoke("AutoMorseWrite", nextMorseCharTime);
        }
    }
    string TextToMorseCode()
    {
        string morseCode = "";
        foreach(char c in tutorialText) {
            if (SoundController.AlphabetToMorse.ContainsKey(c.ToString())) {
                morseCode += SoundController.AlphabetToMorse[c.ToString()];
            }
        }
        return morseCode;
    }
    void AutoWrite()
    {
        decodedText.text += tutorialText[nextChar];
        nextChar++;
    }
    void AutoMorseWrite()
    {
        morseText.text += tutorialMorseText[nextMorseChar];
        nextMorseChar++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecodeText : MonoBehaviour {

    public static string morseToSoundEncoding;
    [SerializeField] UserInput UserInput;
    [SerializeField] Text inputText;
    [SerializeField] Text decodedInputText;

    int letterCounter = 0;
    string currentLetter = "";
    string currentLetterDecoded = "";

    int lastSlashIndex = 0;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        if (FindNewLetter()) DecodeNew();
        else Decode();
    }

    bool FindNewLetter()
    {
        if(lastSlashIndex > 0) {
            currentLetter = inputText.text.Substring(lastSlashIndex, inputText.text.Length - lastSlashIndex);
        } else {
            currentLetter = inputText.text;
        }

        int newSlashIndex = 0;
        int counter = 0;
        foreach (char c in inputText.text) {
            counter++;
            if (c == ' ' || c == '#') {
                //Debug.Log(lastSlashIndex + " NEW: " + newSlashIndex);
                newSlashIndex = counter;
            }
        }

        if (newSlashIndex > lastSlashIndex) {
            lastSlashIndex = newSlashIndex;
            
            currentLetter = inputText.text.Substring(lastSlashIndex, inputText.text.Length - lastSlashIndex);
            Debug.Log(currentLetter);
            return true;
        } else {
            return false;
        }
    }

    void DecodeNew()
    {
        decodedInputText.text += currentLetterDecoded;
        letterCounter++;

        int counter = 0;
        foreach (char c in morseToSoundEncoding) {
            if (c == '/' || c == '+') {
                counter++;
                if(c == '+' && letterCounter == counter) {
                    RemoveOneLetter();
                    decodedInputText.text += "   ";
                }
            }
        } 
    }

    void Decode()
    {
        if (currentLetter == "$") {
            RemoveOneLetter();
            decodedInputText.text += "#";
        } else {
            currentLetterDecoded = DecodeLetter(currentLetter);
            RemoveOneLetter();
            decodedInputText.text += currentLetterDecoded;
        }
        
    }

    string DecodeLetter(string morseCode)
    {
        string decodedCode;
        if (SoundController.MorseToAlphabet.TryGetValue(morseCode, out decodedCode)) {
            return decodedCode;
        } else {
            return "?";
        }
    }

    void RemoveOneLetter()
    {
        if (decodedInputText.text.Length > 0) decodedInputText.text = decodedInputText.text.Substring(0, decodedInputText.text.Length - 1);
    }

    public void ResetDecode () {
        letterCounter = 0;
        lastSlashIndex = 0;
        currentLetter = "";
        currentLetterDecoded = "";

        decodedInputText.text = "";
    }

    public void ChuckNoris (string text) {
        decodedInputText.text = text;
    }
}

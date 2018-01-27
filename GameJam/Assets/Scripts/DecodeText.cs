using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecodeText : MonoBehaviour {

    [SerializeField] Text inputText;
    Text decodedInputText;

    string currentLetter = "";
    string currentLetterDecoded = "";

    int lastSlashIndex = 0;

    private void Start()
    {
        decodedInputText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if (FindNewLetter()) DecodeNew();
        else Decode();
    }

    bool FindNewLetter()
    {
        if(lastSlashIndex > 0) {
            currentLetter = inputText.text.Substring(lastSlashIndex + 1, inputText.text.Length - lastSlashIndex - 1);
        } else {
            currentLetter = inputText.text;
        }

        int newSlashIndex = 0;
        int counter = 0;
        foreach (char c in inputText.text) {
            counter++;
            if (c == '/') {
                newSlashIndex = counter;
            }
        }

        if (newSlashIndex > lastSlashIndex) {
            lastSlashIndex = newSlashIndex;
            currentLetter = inputText.text.Substring(lastSlashIndex + 1, inputText.text.Length - lastSlashIndex - 1);
            return true;
        } else {
            return false;
        }
    }

    void DecodeNew()
    {
        decodedInputText.text += currentLetterDecoded;
    }

    void Decode()
    {
        currentLetterDecoded = DecodeLetter(currentLetter);
        if(decodedInputText.text.Length > 0) decodedInputText.text = decodedInputText.text.Substring(0, decodedInputText.text.Length-1);
        decodedInputText.text += currentLetterDecoded;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public string text;

    AudioSource audioSource;
    public AudioClip Long;
    public AudioClip Short;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TextToMorseSignal(text);
    }

    void TextToMorseSignal(string text)
    {
        text = TextToMorseString(text);
        float InvokeNextAtTime = 0;
        float TimeBetweenSigns = 0.4f;
        float LongSignalLength = 0.8f;
        float ShortSignalLength = 0.3f;
        float TimeBetweenLetters = 1.2f;

        foreach (char c in text) {
            switch (c) {
                case '.': Invoke("PlayShort", InvokeNextAtTime); InvokeNextAtTime += (TimeBetweenSigns + ShortSignalLength); break;
                case '-': Invoke("PlayLong", InvokeNextAtTime); InvokeNextAtTime += (TimeBetweenSigns + LongSignalLength); break;
                case ' ': InvokeNextAtTime += TimeBetweenLetters; break;
            }
        }
    }

    string TextToMorseString(string text)
    {
        text = text.ToLower();
        string morse_text = "";
        foreach (char c in text) {
            switch (c) {
                case 'a': morse_text += ".-"; break;
                case 'b': morse_text += "-..."; break;
                case 'c': morse_text += "-.-."; break;
                case 'd': morse_text += "-.."; break;
                case 'e': morse_text += "."; break;
                case 'f': morse_text += "..-."; break;
                case 'g': morse_text += "--."; break;
                case 'h': morse_text += "...."; break;
                case 'i': morse_text += ".."; break;
                case 'j': morse_text += ".---"; break;
                case 'k': morse_text += "-.-"; break;
                case 'l': morse_text += ".-.."; break;
                case 'm': morse_text += "--"; break;
                case 'n': morse_text += "-."; break;
                case 'o': morse_text += "---"; break;
                case 'p': morse_text += ".--."; break;
                case 'q': morse_text += "--.-"; break;
                case 'r': morse_text += ".-."; break;
                case 's': morse_text += "..."; break;
                case 't': morse_text += "-"; break;
                case 'u': morse_text += "..-"; break;
                case 'v': morse_text += "...-"; break;
                case 'w': morse_text += ".--"; break;
                case 'x': morse_text += "-..-"; break;
                case 'y': morse_text += "-.--"; break;
                case 'z': morse_text += "--.."; break;
                case ' ': morse_text += "/ "; break;
            }
            morse_text += ' ';
        }
        Debug.Log(morse_text);
        return morse_text;
    }

    void PlayLong()
    {
        audioSource.clip = Long;
        audioSource.Play();
    }
    void PlayShort()
    {
        audioSource.clip = Short;
        audioSource.Play();
    }
}

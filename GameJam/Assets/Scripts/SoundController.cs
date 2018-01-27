using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    [SerializeField] AudioSource AudioSource;
    [SerializeField] Text Show_text;

    [SerializeField] List<LevelData> AllLevelData;

    Dictionary<string, string> MorseAlphabet = new Dictionary<string, string>() {
        {"A",".-" },
        { "B","-..." },
        { "C","-.-." },
        { "D","-.." },
        { "E","." },
        { "F","..-." },
        { "G","--." },
        { "H","...." },
        { "I",".." },
        { "J",".---" },
        { "K","-.-" },
        { "L",".-.." },
        { "M","--" },
        { "N","-." },
        { "O","---" },
        { "P",".--." },
        { "Q","--.-" },
        { "R",".-." },
        { "S","..." },
        { "T","-" },
        { "U","..-" },
        { "V","...-" },
        { "W",".--" },
        { "X","-..-" },
        {"Y","-.--" },
        { "Z","--.." },
        { "0","-----" },
        { "1",".----" },
        { "2","..---" },
        { "3","...--" },
        { "4","....-" },
        { "5","....." },
        { "6","-...." },
        { "7","--..." },
        { "8","---.." },
        { "9","----." },
        { ".",".-.-.-" },
        { ",","--..--" },
        { "-","-....-" },
        {"?","..--.." },
        { ":","---..." },
        { "@",".--.-." }
    };

    enum MorseSoundNames {Short, Long, PauseBetweenSimbols, PauseBetweenCharacters, PauseBetweenWords };
    // TODO: get this contants from audio file
    const float ShortTime = 0.3f;
    const float LongTime = 0.8f;
    const float Pause = 1f;

    float TimeUntilNextSound = 0.0f;

    Queue<MorseSoundNames> SoundsToPlay = new Queue<MorseSoundNames>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            MorseToSound(TextToMorse("TEST IS ON"));

            //PickSoundToPlay();
            ShowText();
        }

        PlayEnuedSounds();

    }

    void PickSoundToPlay (MorseSoundNames msn) {
        AudioSource.clip = AllLevelData[0].MorseAudioShort;
        AudioSource.Stop();
        AudioSource.Play();
    }

    void ShowText () {
        Show_text.text = AllLevelData[0].TextFromAudio;
    }

    string TextToMorse (string text) {
        string[] words = text.Split(' ');
        string morseEncoding = "";
        foreach (string word in words) {
            for (int i = 0; i < word.Length; ++i) {
                string encoding;
                if (MorseAlphabet.TryGetValue(word.Substring(i,1), out encoding)) {
                    morseEncoding += encoding;
                }
                morseEncoding += "/";
            }
            morseEncoding = morseEncoding.Substring(0, morseEncoding.Length - 1);
            morseEncoding += "+";
        }

        return morseEncoding;
            
    }

    void MorseToSound (string encoding) {
        for (int i = 0; i < encoding.Length; ++i) {
            switch (encoding[i]) {
                case '.':
                    SoundsToPlay.Enqueue(MorseSoundNames.Short);
                    break;
                case '-':
                    SoundsToPlay.Enqueue(MorseSoundNames.Long);
                    break;

                case '/':
                    SoundsToPlay.Enqueue(MorseSoundNames.PauseBetweenCharacters);
                    break;

                case '+':
                    SoundsToPlay.Enqueue(MorseSoundNames.PauseBetweenWords);
                    break;
            }
        }

    }

    void PlayEnuedSounds () {
        if (SoundsToPlay.Count == 0) {
            return;
        }
        if (TimeUntilNextSound > 0) {
            TimeUntilNextSound -= Time.deltaTime;
            return;
        }

        MorseSoundNames msn = SoundsToPlay.Dequeue();

        PickSoundToPlay(msn);

        switch (msn) {
            case MorseSoundNames.Short:
                TimeUntilNextSound = ShortTime;
                break;

            case MorseSoundNames.Long:
                TimeUntilNextSound = LongTime;
                break;

            case MorseSoundNames.PauseBetweenWords:
                TimeUntilNextSound = Pause;
                break;

            case MorseSoundNames.PauseBetweenCharacters:
                TimeUntilNextSound = Pause;
                break;
        }
    }

}

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
        { "@",".--.-." },
        { "*", "*"}
    };

    [SerializeField] List<string> LevelText = new List<string>() {
        "SOS SOS",
        "SOS * SOS",
        "TEST IS ON"
    };

    enum MorseSoundNames {Short, Long, PauseBetweenSimbols, PauseBetweenCharacters, PauseBetweenWords, NotClearSound };
    // TODO: get this contants from audio file
    const float ShortTime = 0.3f;
    const float LongTime = 0.8f;
    const float Pause = 1.2f;
    const float TimeBetweenSignals = 0.4f;

    const float MinNotClearSound = 0.5f;
    const float MaxNotClearSound = 1.2f;

    float TimeUntilNextSound = 0.0f;

    Queue<MorseSoundNames> SoundsToPlay = new Queue<MorseSoundNames>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // manjka en if statement (bool startGame iz GameManager-ja) zato, da se naslednja koda lahko izvede samo enkrat
        /*if (Input.GetKeyDown(KeyCode.Return)) {
            string morseEncoding = TextToMorse(LevelText[0]);
            MorseToSound(morseEncoding);
            Debug.Log(morseEncoding);
        }*/

        PlayEnuedSounds();
    }

    void PickSoundToPlay (MorseSoundNames msn) {
        switch (msn) {
            case MorseSoundNames.Short:
                AudioSource.clip = AllLevelData[0].MorseAudioShort;
                break;
            case MorseSoundNames.Long:
                AudioSource.clip = AllLevelData[0].MorseAudioLong;
                break;

            case MorseSoundNames.NotClearSound:
                AudioSource.clip = AllLevelData[0].MorseAudioNotClear;
                break;

            case MorseSoundNames.PauseBetweenSimbols:
            case MorseSoundNames.PauseBetweenCharacters:
            case MorseSoundNames.PauseBetweenWords:
                AudioSource.clip = null;
                break;
        }

        AudioSource.Stop();
        if (AudioSource.clip != null) {
            AudioSource.Play();
        }
        
    }


    string TextToMorse (string text) {
        text = text.ToUpper();
        string[] words = text.Split(' ');
        string morseEncoding = "";
        foreach (string word in words) {
            Debug.Log(word);
            if (word.Equals("*", System.StringComparison.Ordinal)) {
                Debug.Log("here");
                morseEncoding += "*";
            } else {

                for (int i = 0; i < word.Length; ++i) {
                    string encoding;
                    if (MorseAlphabet.TryGetValue(word.Substring(i, 1), out encoding)) {
                        morseEncoding += encoding;
                    }
                    morseEncoding += "/";
                }

                morseEncoding = morseEncoding.Substring(0, morseEncoding.Length - 1);
            }
            
            morseEncoding += "+";
        }

        return morseEncoding;
            
    }

    void MorseToSound (string encoding) {
        SoundsToPlay.Enqueue(MorseSoundNames.PauseBetweenWords);
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
                case '*':
                    SoundsToPlay.Enqueue(MorseSoundNames.NotClearSound);
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
            case MorseSoundNames.NotClearSound:
                TimeUntilNextSound = Random.Range(MinNotClearSound, MaxNotClearSound);
                break;
        }

        TimeUntilNextSound += TimeBetweenSignals;
    }


    public void PlayLevel (int levelNumber) {
        string morseEncoding = TextToMorse(LevelText[levelNumber]);
        MorseToSound(morseEncoding);
        Debug.Log(morseEncoding);
    }
}

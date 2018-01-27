using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip MorseAudioShort;
    [SerializeField] AudioClip MorseAudioLong;
    [SerializeField] AudioClip MorseAudioNotClear;
    [SerializeField] Text Show_text;

    public static Dictionary<string, string> AlphabetToMorse = new Dictionary<string, string>() {
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

    public static Dictionary<string, string> MorseToAlphabet = new Dictionary<string, string>() {
        { ".-","A" },
        { "-...","B" },
        { "-.-.","C" },
        { "-..","D" },
        { ".","E" },
        { "..-.","F" },
        { "--.","G" },
        { "....","H" },
        { "..","I" },
        { ".---","J" },
        { "-.-","K" },
        { ".-..","L" },
        { "--","M" },
        { "-.","N" },
        { "---","O" },
        { ".--.","P" },
        { "--.-","Q" },
        { ".-.","R" },
        { "...","S" },
        { "-","T" },
        { "..-","U" },
        { "...-","V" },
        { ".--","W" },
        { "-..-","X" },
        { "-.--","Y" },
        { "--..","Z" },
        { "-----","0" },
        { ".----","1" },
        { "..---","2" },
        { "...--","3" },
        { "....-","4" },
        { ".....","5" },
        { "-....","6" },
        { "--...","7" },
        { "---..","8" },
        { "----.","9" },
        { ".-.-.-","." },
        { "--..--","," },
        { "-....-","-" },
        { "..--..","?" },
        { "---...",":" },
        { ".--.-.","@" },
        { "*", "*" }
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
                AudioSource.clip = MorseAudioShort;
                break;
            case MorseSoundNames.Long:
                AudioSource.clip = MorseAudioLong;
                break;

            case MorseSoundNames.NotClearSound:
                AudioSource.clip = MorseAudioNotClear;
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
            if (word.Equals("*", System.StringComparison.Ordinal)) {
                morseEncoding += "*";
            } else {

                for (int i = 0; i < word.Length; ++i) {
                    string encoding;
                    if (AlphabetToMorse.TryGetValue(word.Substring(i, 1), out encoding)) {
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


    public void PlayLevel (string textPlay) {
        string morseEncoding = TextToMorse(textPlay);
        MorseToSound(morseEncoding);
        DecodeText.morseToSoundEncoding = morseEncoding;
    }

    public void ResetQueue () {
        SoundsToPlay.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class LevelData : ScriptableObject {
    public AudioClip MorseAudioShort;
    public AudioClip MorseAudioLong;
    public AudioClip MorseAudioNotClear;
    public string TextFromAudio = "Test";
}

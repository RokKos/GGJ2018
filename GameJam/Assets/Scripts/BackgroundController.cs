using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    [SerializeField] AudioClip IntroSound;
    [SerializeField] AudioClip LoopSoundStart;
    [SerializeField] AudioClip LoopSoundLoop;
    [SerializeField] AudioSource AudioSource;

    bool introSound = true;

    // Use this for initialization
    void Start () {
        AudioSource.clip = IntroSound;
        AudioSource.PlayOneShot(IntroSound);

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(AudioSource.isPlaying);
        if (introSound && !AudioSource.isPlaying) {
            introSound = false;
            AudioSource.clip = LoopSoundStart;
            AudioSource.PlayOneShot(LoopSoundStart);
        }

        if (!introSound && !AudioSource.isPlaying) {
            AudioSource.clip = LoopSoundLoop;
            AudioSource.loop = true;
            AudioSource.Play();
        }


	}
}

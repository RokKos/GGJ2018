using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    [SerializeField] AudioClip IntroSound;
    [SerializeField] AudioSource AudioSource;

    // Use this for initialization
    void Start () {
        AudioSource.clip = IntroSound;
        AudioSource.PlayOneShot(IntroSound);

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(AudioSource.isPlaying);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioTvOnAt : MonoBehaviour {

    [SerializeField] float delay = 0.2f;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().PlayDelayed(delay);
	}
}

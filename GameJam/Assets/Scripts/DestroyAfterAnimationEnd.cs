using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimationEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyAnimation", GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
    }
    void DestroyAnimation()
    {
        Destroy(gameObject);
    }
}

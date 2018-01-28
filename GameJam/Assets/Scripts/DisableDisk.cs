using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDisk : MonoBehaviour {

	public void disableChildDisk()
    {
        Debug.Log("Disk disabled!");
        gameObject.transform.GetChild(2).gameObject.active = false;
    }
}

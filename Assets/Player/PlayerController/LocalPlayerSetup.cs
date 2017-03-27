using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerSetup : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<PlayerController> ().enabled = true;
			GetComponentInChildren<Camera> ().enabled = true;
		}
	}

}

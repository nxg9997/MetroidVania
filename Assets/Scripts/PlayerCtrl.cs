using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	Physics pPhys;

	// Use this for initialization
	void Start () {
		pPhys = GetComponent<Physics> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Movement(){
		if (Input.GetKey (KeyCode.A) == true) {
			//pPhys.
		}
	}
}

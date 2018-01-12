using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour {

	public bool CanDamage = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CanDamage) {
			gameObject.tag = "pain";
		}
		else {
			gameObject.tag = "nopain";
		}
	}
}

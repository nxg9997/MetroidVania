using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GenFiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!CheckExistance ("dLoc")) {
			CreateDeathLocationList ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool CheckExistance(string name){
		bool exists = File.Exists ("name");
		return exists;
	}

	void CreateDeathLocationList(){
		File.Create ("dLoc");
	}
}

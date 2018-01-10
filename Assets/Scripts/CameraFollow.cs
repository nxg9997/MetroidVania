using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Camera cam;
	float startZ;

	// Use this for initialization
	void Start () {
		startZ = cam.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		cam.transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
	}
}

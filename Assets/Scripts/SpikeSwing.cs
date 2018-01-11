using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSwing : MonoBehaviour {

	public float maxAngle;
	public float aps; //angle per second
	public int direction; // 0 for left, 1 for right
	public float startAng;
	public bool Circle = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Circle) {
			if (direction == 0) {
				startAng -= aps;
				transform.rotation = Quaternion.AngleAxis (startAng, Vector3.back);
			}
			else if (direction == 1) {
				startAng += aps;
				transform.rotation = Quaternion.AngleAxis (startAng, Vector3.back);
			}
		}
		else {
			if (direction == 0) {
				startAng -= aps;
				transform.rotation = Quaternion.AngleAxis (startAng, Vector3.back);
				if (startAng < -maxAngle) {
					direction = 1;
				}
			} else if (direction == 1) {
				startAng += aps;
				transform.rotation = Quaternion.AngleAxis (startAng, Vector3.back);
				if (startAng > maxAngle) {
					direction = 0;
				}
			}
		}
	}
}

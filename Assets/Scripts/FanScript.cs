using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour {

	public GameObject player;
	public Vector3 force;
	public int direction = 0; //0 for up, 1 for down

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col){
		player.GetComponent<Rigidbody2D> ().AddForce (force);

	}

	void OnTriggerEnter2D(Collider2D col){
		player.GetComponent<Playerv2> ().CanJump = true;
	}
}

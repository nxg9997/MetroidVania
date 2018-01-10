using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Vehicle {

	public Vector3 jumpVec;
	public Vector3 speedVec;
	public Vector3 gravityVec;

	bool CanGravity = true;

	//float maxHeight = 0f;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	/*// Update is called once per frame
	void Update () {
		
	}*/

	protected override void CalcSteeringForces(){
		Vector3 ultimate = Vector3.zero;
		ultimate += Control ();
		/*if (CanGravity) {
			ultimate += Gravity ();
		}*/
		Vector3.ClampMagnitude (ultimate, maxForce);
		acceleration += ultimate;
		CheckKeys ();
	}

	Vector3 Control(){
		Vector3 ultimate = Vector3.zero;
		if (Input.GetKeyDown (KeyCode.A)) {
			ultimate -= speedVec;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			ultimate += speedVec;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			ultimate += Jump ();
			Debug.Log ("jumping");
		}
		return ultimate;
	}

	void CheckKeys(){
		if (Input.GetKeyUp (KeyCode.A)) {
			if (velocity.x < 0) {
				velocity.x = 0;
			}
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			if (velocity.x > 0) {
				velocity.x = 0;
			}
		}
	}

	Vector3 Jump(){
		return jumpVec;
	}

	Vector3 Gravity(){
		return gravityVec;
	}

	/*void CheckBelow(){
		RaycastHit2D rch = Physics2D.Raycast (transform.position, Vector2.down);
		maxHeight = rch.point.y;
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("collision detected");
		velocity.y = 0;
		//CanGravity = false;
		transform.position = new Vector3(transform.position.x, maxHeight);
	}

	void OnCollisionExit2D(Collision2D col){
		//CanGravity = true;
	}*/
}

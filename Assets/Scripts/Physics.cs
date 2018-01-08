using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

	//variables
	public bool IsPlayer = false;

	public Vector3 position;
	public Vector3 acceleration = Vector3.zero;
	public Vector3 velocity = Vector3.zero;

	public float maxSpeed;
	public float maxForce;

	public Vector3 speed;
	public Vector3 friction;
	public Vector3 gravity;

	// Use this for initialization
	void Start () {
		position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsPlayer) {
			Movement ();
			/*if (Mathf.Abs(velocity.sqrMagnitude) > 0) {
				ApplyForce (-friction);
			}*/
		}
		ApplyForce (-gravity);
		CalcSteeringForces ();
	}

	void CalcSteeringForces(){
		Vector3 ultimate = Vector3.zero;
		ultimate += acceleration;
		Vector3.ClampMagnitude (ultimate, maxForce);
		velocity += ultimate;
		Vector3.ClampMagnitude (velocity, maxSpeed);
		position += velocity;
		gameObject.transform.position = position;
		acceleration = Vector3.zero;
		velocity = Vector3.zero;
	}

	public void ApplyForce(Vector3 force){
		acceleration += force;
	}

	void Movement(){
		if (Input.GetKey (KeyCode.A) == true) {
			ApplyForce (-speed);
		}
		if (Input.GetKey (KeyCode.D) == true) {
			ApplyForce (speed);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "platform") {
			if (Vector3.Dot (gameObject.transform.up, col.gameObject.transform.position) < 0) {
				velocity.y = 0f;
				acceleration.y = 0f;
			}
		}
	}
}

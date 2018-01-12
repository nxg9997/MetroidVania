using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : Vehicle {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("charPH");
		base.Start ();
	}
	
	/*// Update is called once per frame
	void Update () {
		
	}*/

	protected override void CalcSteeringForces(){
		acceleration += Vector3.ClampMagnitude (Seek (player.transform.position), maxForce);
	}

	void OnCollisionEnter2D(Collision2D col){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		Destroy (gameObject);
	}
}

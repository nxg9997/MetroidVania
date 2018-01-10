using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerv2 : MonoBehaviour {

	public Vector3 jumpVec;
	public Vector3 speedVec;
	public float maxSpeed;
	public float speedRedux;
	private float maxSpeedSq;
	private bool CanJump = false;

	// Use this for initialization
	void Start () {
		maxSpeedSq = Mathf.Pow (maxSpeed, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ultimate = Vector3.zero;
		if (Input.GetKeyDown (KeyCode.Space) && CanJump) {
			//GetComponent<Rigidbody2D> ().AddForce (jumpVec);
			ultimate += jumpVec;
			CanJump = false;
		}

		/*if (GetComponent<Rigidbody2D> ().velocity.sqrMagnitude >= maxSpeedSq) {
			Vector3.ClampMagnitude (GetComponent<Rigidbody2D> ().velocity, maxSpeed);
			GetComponent<Collider2D> ().sharedMaterial.friction = 0f;
		}
		else {
			if (Input.GetKey (KeyCode.A) && GetComponent<Rigidbody2D> ().velocity.sqrMagnitude < maxSpeedSq) {
				GetComponent<Rigidbody2D> ().AddForce (-speedVec);
				GetComponent<Collider2D> ().sharedMaterial.friction = 0f;
			}
			else if (Input.GetKey (KeyCode.D) && GetComponent<Rigidbody2D> ().velocity.sqrMagnitude < maxSpeedSq) {
				GetComponent<Rigidbody2D> ().AddForce (speedVec);
				GetComponent<Collider2D> ().sharedMaterial.friction = 0f;
			}
			else {
				GetComponent<Collider2D> ().sharedMaterial.friction = .4f;
			}
		}*/

		if (Input.GetKey (KeyCode.A)) {
			ultimate -= speedVec;
		}
		if (Input.GetKey (KeyCode.D)) {
			ultimate += speedVec;
		}
		//Vector3.ClampMagnitude (ultimate, maxSpeed);
		GetComponent<Rigidbody2D> ().AddForce (ultimate);
		if (GetComponent<Rigidbody2D>().velocity.sqrMagnitude > maxSpeedSq) {
			Vector3.ClampMagnitude (GetComponent<Rigidbody2D> ().velocity, maxSpeed);
		}

	}

	void OnCollisionEnter2D(Collision2D col){



		CanJump = true;

		/*RaycastHit2D lRch = Physics2D.Raycast (transform.position, Vector2.left);
		RaycastHit2D rRch = Physics2D.Raycast (transform.position, Vector2.right);
		RaycastHit2D dRch = Physics2D.Raycast (transform.position, Vector2.down);

		if (lRch.distance < dRch.distance) {
			
		}*/
	}

	void OnCollisionStay2D(Collision2D col){
		CanJump = true;
	}

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.tag == "slowzone") {
			Debug.Log ("triggered");
			GetComponent<Rigidbody2D> ().AddForce (-GetComponent<Rigidbody2D> ().velocity * speedRedux);
		}
	}
}

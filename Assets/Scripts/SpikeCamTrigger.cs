using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCamTrigger : MonoBehaviour {

	public Camera cCam;
	public Camera mCam;

	public Vector3 startPos;
	public GameObject targetObj;

	public GameObject player;

	public Vector3 position;
	public Vector3 direction;
	public Vector3 velocity = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;

	public float mass;
	public float maxSpeed;
	public float maxForce;

	public bool CanStart = false;
	public bool CanInit = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CanStart) {
			if (CanInit) {
				mCam.gameObject.SetActive (false);
				cCam.gameObject.SetActive (true);
				startPos = mCam.transform.position;
				cCam.transform.position = mCam.transform.position;
				player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
				player.GetComponent<Playerv2> ().CanMove = false;
				CanInit = false;
			}
			UpdatePosition ();
		}

	}

	protected Vector3 Seek (Vector3 targetPos) {
		Vector3 desiredVelocity = targetPos - position;
		desiredVelocity.Normalize ();
		desiredVelocity = desiredVelocity * maxSpeed;

		return (desiredVelocity - velocity);
	}

	void UpdatePosition () {
		acceleration += Seek (targetObj.transform.position);
		position = cCam.transform.position;
		velocity += acceleration;
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
		position += velocity;
		//direction = velocity.normalized;
		acceleration = Vector3.zero;
		cCam.transform.position = position;
	}
}

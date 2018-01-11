using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerv2 : MonoBehaviour {

	public Vector3 jumpVec;
	public Vector3 speedVec;
	public float maxSpeed;
	public float speedRedux;
	public float scaleAmt;
	public float leastScale;
	private float startScaleY;
	private int vertScaleCtrl = 0;
	public bool CanMove = false;
	private float maxSpeedSq;
	private bool CanJump = false;
	private bool CanLandAnim = false;

	// Use this for initialization
	void Start () {
		maxSpeedSq = Mathf.Pow (maxSpeed, 2f);
		startScaleY = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ultimate = Vector3.zero;
		if (Input.GetKeyDown (KeyCode.Space) && CanJump && CanMove) {
			//GetComponent<Rigidbody2D> ().AddForce (jumpVec);
			ultimate += jumpVec;
			CanJump = false;
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			CanMove = true;
		}

		if (CanMove) {
			if (Input.GetKey (KeyCode.A)) {
				ultimate -= speedVec;
			}
			if (Input.GetKey (KeyCode.D)) {
				ultimate += speedVec;
			}
			//Vector3.ClampMagnitude (ultimate, maxSpeed);
			GetComponent<Rigidbody2D> ().AddForce (ultimate);
			if (GetComponent<Rigidbody2D> ().velocity.sqrMagnitude > maxSpeedSq) {
				Vector3.ClampMagnitude (GetComponent<Rigidbody2D> ().velocity, maxSpeed);
			}
		}

		/*if (CanLandAnim) {
			LandingAnim ();
		}*/
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "pain") {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}

		CanJump = true;

		/*int layerMask = 1 << 8;

		layerMask = ~layerMask;

		RaycastHit2D lRch = Physics2D.Raycast (transform.position, Vector2.left, 5f, layerMask);
		RaycastHit2D rRch = Physics2D.Raycast (transform.position, Vector2.right, 5f, layerMask);
		RaycastHit2D dRch = Physics2D.Raycast (transform.position, Vector2.down, 5f, layerMask);

		Debug.Log ("left - down: " + (lRch.distance - dRch.distance) + " r-d: " + (rRch.distance - dRch.distance));*/
		Debug.Log (GetComponent<Rigidbody2D> ().velocity.y);

		if (Mathf.Round(GetComponent<Rigidbody2D>().velocity.y) == 0) {

			CanLandAnim = true;
		}


		/*if (lRch.distance < dRch.distance) {
			
		}*/
	}

	void OnCollisionStay2D(Collision2D col){
		CanJump = true;
	}

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.tag == "slowzone") {
			//Debug.Log ("triggered");
			GetComponent<Rigidbody2D> ().AddForce (-GetComponent<Rigidbody2D> ().velocity * speedRedux);
		}


	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "trigger") {
			col.gameObject.GetComponent<SpikeCamTrigger> ().CanStart = true;
			CanMove = false;
		}
	}

	void LandingAnim(){
		if (vertScaleCtrl == 0) {
			gameObject.transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y - scaleAmt);
		}
		else {
			gameObject.transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y + scaleAmt);
		}

		if (transform.localScale.y <= leastScale) {
			vertScaleCtrl = 1;
		}
		else if (transform.localScale.y >= startScaleY) {
			CanLandAnim = false;
			vertScaleCtrl = 0;
		}
	}
}

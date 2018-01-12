using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

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
	public bool CanJump = false;
	private bool CanLandAnim = false;
	public GameObject deathMarker;
	public Vector3 startPos;
	public Text overlay;
	private int deathCount = 0;

	// Use this for initialization
	void Start () {
		maxSpeedSq = Mathf.Pow (maxSpeed, 2f);
		startScaleY = transform.localScale.y;

		if (SceneManager.GetActiveScene().buildIndex != 0) {
			CanMove = true;
		}
		//ReadDeaths ();
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (CanJump);
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
			//OnDeath ();
			deathCount++;
			GameObject dMark = Instantiate (deathMarker, gameObject.transform.position, Quaternion.identity);
			overlay.text = "Death Count: " + deathCount;
			//dMark.transform.position = startPos;
			//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			transform.position = startPos;
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}

		if (col.gameObject.tag == "platform") {
			CanJump = true;
		}
		/*int layerMask = 1 << 8;

		layerMask = ~layerMask;

		RaycastHit2D lRch = Physics2D.Raycast (transform.position, Vector2.left, 5f, layerMask);
		RaycastHit2D rRch = Physics2D.Raycast (transform.position, Vector2.right, 5f, layerMask);
		RaycastHit2D dRch = Physics2D.Raycast (transform.position, Vector2.down, 5f, layerMask);

		Debug.Log ("left - down: " + (lRch.distance - dRch.distance) + " r-d: " + (rRch.distance - dRch.distance));*/
		//Debug.Log (GetComponent<Rigidbody2D> ().velocity.y);

		if (Mathf.Round(GetComponent<Rigidbody2D>().velocity.y) == 0) {

			CanLandAnim = true;
		}


		/*if (lRch.distance < dRch.distance) {
			
		}*/
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "platform") {
			CanJump = true;
		}

		if (col.gameObject.tag == "pain") {
			//OnDeath ();
			deathCount++;
			GameObject dMark = Instantiate (deathMarker, gameObject.transform.position, Quaternion.identity);
			overlay.text = "Death Count: " + deathCount;
			//dMark.transform.position = startPos;
			//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			transform.position = startPos;
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		CanJump = false;
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

		/*if (col.gameObject.tag == "boss") {
			deathCount++;
			GameObject dMark = Instantiate (deathMarker, gameObject.transform.position, Quaternion.identity);
			overlay.text = "Death Count: " + deathCount;
			//dMark.transform.position = startPos;
			//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			transform.position = startPos;
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}*/
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

	void ReadDeaths(){
		StreamReader sr = new StreamReader ("dLoc");
		//Vector2[] dArr = new Vector2[1000];
		string line = null;
		while ((line = sr.ReadLine ()) != null/*int i = 0; i < dArr.Length; i++*/) {
			//line = sr.ReadLine ();
			//string line = sr.ReadLine ();
			/*if (line == null) {
				break;
			}*/
			string xStr = line.Substring (0, line.IndexOf (','));
			string yStr = line.Substring (line.IndexOf (',') + 1);
			float x;
			float y;
			float.TryParse (xStr, out x);
			float.TryParse (yStr, out y);
			GameObject dMark = Instantiate (deathMarker, deathMarker.transform);
			dMark.transform.position = new Vector2 (x, y);
			//dArr [i] = new Vector2 (x, y);
		}
		sr.Close ();
	}

	void OnDeath(){
		StreamReader sr = new StreamReader ("dLoc");
		List<string> sList = new List<string> ();
		string line = null;
		while ((line = sr.ReadLine ()) != null) {
			sList.Add (line);
		}
		string[] sArr = sList.ToArray ();
		StreamWriter sw = new StreamWriter ("dLoc");
		for (int i = 0; i < sArr.Length; i++) {
			sw.WriteLine (sArr [i]);
		}
		string newEntry = "" + transform.position.x + "," + transform.position.y;
		sw.WriteLine (newEntry);
		sw.Close ();
	}
}

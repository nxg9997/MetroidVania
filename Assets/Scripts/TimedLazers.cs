using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLazers : MonoBehaviour {

	public float deltaT;
	public bool active;
	public float startDelay;

	// Use this for initialization
	void Start () {
		if (startDelay == 0f) {
			StartCoroutine (SwapActive ());
		}
		else {
			StartCoroutine (Delay ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (startDelay);
		StartCoroutine (SwapActive ());
	}

	IEnumerator SwapActive(){
		active = !active;
		gameObject.GetComponent<SpriteRenderer> ().enabled = active;
		GetComponent<LazerScript> ().CanDamage = active;
		GetComponent<BoxCollider2D> ().enabled = active;

		yield return new WaitForSeconds (deltaT);

		StartCoroutine (SwapActive ());
	}
}

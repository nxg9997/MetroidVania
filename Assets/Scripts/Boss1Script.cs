using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour {

	public GameObject player;
	public GameObject missle;
	public float deltaT;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnMissle ());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = transform.position - player.transform.position;
		dir.Normalize ();
		gameObject.transform.right = dir;
	}

	IEnumerator SpawnMissle(){
		yield return new WaitForSeconds (deltaT);
		GameObject newMissle = Instantiate (missle, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity);
		StartCoroutine (SpawnMissle ());
	}
}

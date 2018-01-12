using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour {

	public string damageName;
	public int health;
	public GameObject exitWall;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (exitWall);
			Destroy (gameObject);
		}
		
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("triggered");
		if (col.gameObject.name.Contains (damageName)) {
			health--;
			player.transform.position = player.GetComponent<Playerv2> ().startPos;
		}
	}
}

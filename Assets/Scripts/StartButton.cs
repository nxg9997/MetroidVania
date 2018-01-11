using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	Button sB;

	// Use this for initialization
	void Start () {
		sB = GetComponent<Button> ();
		sB.onClick.AddListener (TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
		SceneManager.LoadScene (1);
	}
}

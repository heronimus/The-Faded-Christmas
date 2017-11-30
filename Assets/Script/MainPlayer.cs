using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour {

	HUD hudObject;
	bool itemsInTouch;
	GameObject pickUpObject;

	// Use this for initialization
	void Start () {
		Debug.Log ("Player Start");
		//hudObject = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUD> ();
		itemsInTouch = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(itemsInTouch && Input.GetKeyDown("e")){
			Debug.Log ("PickUp !");
			hudObject.addItemList ("- Book");
			pickUpObject.SetActive (false);

		}
		if (Input.GetKeyDown("space"))
			print("space key was pressed");
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Items")) {
			itemsInTouch = true;
			pickUpObject = collision.gameObject;
			Debug.Log ("Sentuh");
		}
	}
	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag.Equals ("Items")) {
			Debug.Log ("Tidak Sentuh");
		}
	}
}

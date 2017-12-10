using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour {

	public int playerLevel;
	HUD hudObject;
	bool itemsInTouch;
	bool boxInTouch;
	bool monstersInTouch;
	float timer = 0;
	GameObject pickUpObject;


	// Use this for initialization
	void Start () {
		Debug.Log ("Player Start");
		hudObject = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUD> ();

		itemsInTouch = false;
		boxInTouch = false;
		monstersInTouch = false;

		if (playerLevel == 2) {
			hudObject.showInfoLarge ("Your faith is strong Max !! .... "+
				"Go find another Christmas Box to save your Family !\n"+
				"\n\n * Beware of the Christmas Eater, Krampus ! *");
		} else {
			hudObject.showInfoLarge ("Max, your family is gone with the faded of Christmas Spirit .... "+
				"Go find the All Christmas Box to save your Family before your Christmas spirit faded to ! \n"+
				"\n\n * press <E> to collect item.");
		}

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 15f) {
			timer = 0;
			hudObject.spiritMinus ();
		}
		if(itemsInTouch && Input.GetKeyDown("e")){
			Debug.Log ("PickUp ! -> "+pickUpObject.name);
			hudObject.addItemList (pickUpObject.name);
			hudObject.showInfoSmall ("Found : " + pickUpObject.name);
			Destroy(pickUpObject);
			itemsInTouch = false;
		}
		if(boxInTouch && Input.GetKeyDown("e")){
			Debug.Log ("PickUp ! -> "+pickUpObject.name);
			hudObject.addBox();
			hudObject.showInfoSmall ("Found : Christmas Box");
			Destroy(pickUpObject);
			boxInTouch = false;
			hudObject.spiritPlus ();
		}
		if(monstersInTouch && timer >= 8){
			timer = 0;
			hudObject.healthMinus();
			hudObject.showInfoSmall ("Ouchh .. ");
			boxInTouch = false;
		}
		if (Input.GetKeyDown("space"))
			hudObject.hideInfo();
		
		if (Input.GetKeyDown ("h")) {
			hudObject.addItemList ("Door Key X");
			hudObject.addItemList ("Door Key Y");
			hudObject.cheat ();
		}
		if (Input.GetKeyDown ("j")) {
			hudObject.showInfoLarge ("You Save The Christmas!");
			hudObject.cheat2 ();
		}
		if (Input.GetKeyDown ("k")) {
			SceneManager.LoadScene("MainMenu");
		}



	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Items")) {
			itemsInTouch = true;
			pickUpObject = collision.gameObject;
			Debug.Log ("Sentuh");
		}
		if (collision.gameObject.tag.Equals ("BoxItems")) {
			boxInTouch = true;
			pickUpObject = collision.gameObject;
			Debug.Log ("Sentuh Box");
		}
		if (collision.gameObject.tag.Equals ("Monsters")) {
			monstersInTouch = true;
			Debug.Log ("Sentuh Monsters");
		}
	}
	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag.Equals ("Items")) {
			itemsInTouch = false;
			Debug.Log ("Tidak Sentuh");
		}
		if (collision.gameObject.tag.Equals ("BoxItems")) {
			boxInTouch = false;
			Debug.Log ("Tidak Sentuh Box");
		}
		if (collision.gameObject.tag.Equals ("Monsters")) {
			monstersInTouch = false;
			Debug.Log ("Sentuh Monsters");
		}
	}
}

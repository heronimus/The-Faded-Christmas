using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LockQuest : MonoBehaviour {


	public string questName;
	HUD hudObject;
	bool itemsInTouch;

	// Use this for initialization
	void Start () {
		hudObject = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUD> ();
		itemsInTouch = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Players")) {
			Debug.Log ("Quest Check");
			//Quest Door Inside
			if (questName == "DoorInside"){
				if (hudObject.checkItems ("Door Key X")) {
					GameObject.Find ("Door_inside").GetComponent<Animation> ().Play ();
					hudObject.deleteItem ("Door Key X");
				} else {
					hudObject.showInfoSmall ("Door Locked !");
				}

			}
			//Quest Door Outside
			if (questName == "DoorOutside"){
				if ( hudObject.checkItems ("Door Key Y") && hudObject.getBoxComplete()) {
					GameObject.Find("Door_outside").GetComponent<Animation>().Play();
					hudObject.deleteItem ("Door Key Y");
					itemsInTouch = true;
					StartCoroutine (Level1Complete ());
				} else {
					hudObject.showInfoSmall("Door Locked, Outside is Freezing ! ");
				}

			}

			if (questName == "CarEscape"){
				if ( hudObject.getBoxComplete()) {
					itemsInTouch = true;
					StartCoroutine (Level2Complete ());
				} else {
					hudObject.showInfoSmall("Strange, I can't get in ...");
				}

			}
				
		}
	}
	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag.Equals ("Players")) {
			Debug.Log ("Quest Uncheck");
		}
	}

	IEnumerator Level1Complete()
	{
		hudObject.showInfoLarge ("LEVEL COMPLETE");
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene("OutdoorScene");
	}

	IEnumerator Level2Complete()
	{
		hudObject.showInfoLarge ("Congratulation !! LEVEL COMPLETE");
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene("MainMenu");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMain : MonoBehaviour {


	public GameObject monsterObject;
	Animation monsterAnim;
	MonsterWandering monsterWander;

	// Use this for initialization
	void Start () {
		monsterAnim = monsterObject.GetComponent<Animation> ();
		monsterWander = GetComponent<MonsterWandering> ();

		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("sleep");
		monsterWander.StopWandering ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

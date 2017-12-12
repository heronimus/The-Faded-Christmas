using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionBack : FSMAction {

	private Animation monsterAnim;
	private MonsterWandering monsterWander;
	private GameObject monsters;
	private GameObject players;

	private string finishEvent;
	private Vector3 startPosition;


	public actionBack (FSMState owner) : base (owner)
	{
	}

	public void Init (GameObject monsters, GameObject players, Animation monsterAnim, MonsterWandering monsterWander, string finishEvent)
	{
		this.monsters = monsters;
		this.players = players;
		this.monsterAnim = monsterAnim;
		this.monsterWander = monsterWander;

		this.finishEvent = finishEvent;
		startPosition = new Vector3 (108.2f, 4f, 8.4f);
	}

	public override void OnEnter ()
	{
		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("walk");
	}

	public override void OnUpdate ()
	{
		if (Vector3.Distance (monsters.transform.position, startPosition) <= 5) {
			Finish ();
			return;
		}
		monsters.transform.position = Vector3.MoveTowards (monsters.transform.position, startPosition, .03f);
		monsters.transform.LookAt (startPosition);
//		Debug.Log ("Dist b:"+Vector3.Distance (monsters.transform.position, startPosition));
	}

	public override void OnExit ()
	{
		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("idle");
	}

	public void Finish ()
	{
		if (!string.IsNullOrEmpty (finishEvent)) {
			GetOwner ().SendEvent (finishEvent);
		}
	}
}
